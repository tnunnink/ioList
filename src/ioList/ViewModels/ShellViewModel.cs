using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ioList.Shared;
using Ookii.Dialogs.Wpf;
using Squirrel;
using Squirrel.Sources;

namespace ioList.ViewModels
{
    public partial class ShellViewModel : ObservableValidator
    {
        public ShellViewModel()
        {
            GetVersion();
        }
        
        [ObservableProperty] 
        private string _version;

        [ObservableProperty] 
        private int _selectedIndex;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(GenerateCommand))]
        [NotifyDataErrorInfo]
        [Required]
        private string _sourceFile = string.Empty;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(GenerateCommand))]
        [NotifyDataErrorInfo]
        [Required]
        [CustomValidation(typeof(ShellViewModel), nameof(ValidateName))]
        private string _destinationName = string.Empty;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(GenerateCommand))]
        [NotifyDataErrorInfo]
        [Required]
        private string _destinationLocation = string.Empty;

        [ObservableProperty]
        private string _errorMessage;

        [RelayCommand]
        private void SelectSource()
        {
            var dialog = new VistaOpenFileDialog
            {
                Title = "Select L5X file",
                Filter = "L5X Files (*.L5X)|*.L5X",
                DefaultExt = ".L5X",
                AddExtension = true,
                CheckFileExists = true,
                Multiselect = false
            };

            var fileName = dialog.ShowDialog() == true ? dialog.FileName : string.Empty;

            if (string.IsNullOrEmpty(fileName)) return;

            SourceFile = fileName;
        }

        [RelayCommand]
        private void SelectLocation()
        {
            var dialog = new VistaFolderBrowserDialog
            {
                Description = "Select Project Location",
                UseDescriptionForTitle = true,
                Multiselect = false
            };

            var folder = dialog.ShowDialog() == true ? dialog.SelectedPath : string.Empty;

            if (string.IsNullOrEmpty(folder)) return;

            DestinationLocation = folder;
        }

        [RelayCommand]
        private void OpenInExplorer()
        {
            if (!Directory.Exists(DestinationLocation))
                return;
            
            Process.Start("explorer.exe", DestinationLocation);
            
            SelectedIndex = 0;
        }

        [RelayCommand]
        private void TryAgain() => SelectedIndex = 0;

        [RelayCommand]
        private void ReportIssue()
        {
            //todo what should this do...?
            SelectedIndex = 0;
        }

        [RelayCommand(CanExecute = nameof(CanGenerate))]
        private void Generate()
        {
            SelectedIndex++;
            
            Task.Run(() =>
            {
                try
                {
                    var destinationFile = Path.Combine(DestinationLocation, $"{DestinationName}.csv");
                    Generator.Generate(SourceFile, destinationFile);
                    SelectedIndex++;
                }
                catch (Exception e)
                {
                    ErrorMessage = e.Message;
                    SelectedIndex = 4;
                }
            });
        }

        private bool CanGenerate() => !string.IsNullOrWhiteSpace(SourceFile) 
                                      && !string.IsNullOrWhiteSpace(DestinationName)
                                      && !string.IsNullOrWhiteSpace(DestinationLocation)
                                      && !HasErrors;

        public static ValidationResult ValidateName(string value, ValidationContext context)
        {
            var vm = (ShellViewModel)context.ObjectInstance;

            if (value.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
                return new ValidationResult($"The name {value} is not a valid file name.");

            var fileName = Path.Combine(vm.DestinationLocation, $"{value}.db");

            return File.Exists(fileName)
                ? new ValidationResult($"The file name {value} already exists in the specified folder.")
                : ValidationResult.Success;
        }

        private void GetVersion()
        {
            try
            {
                using var manager = new UpdateManager(new GithubSource(App.RepositoryUrl, string.Empty, false));
                var installedVersion = manager.CurrentlyInstalledVersion();
                Version = installedVersion is not null ? installedVersion.Version.ToString() : string.Empty;
                    
            }
            catch (Exception e)
            {
                //todo log
                Console.WriteLine(e);
            }
        }
    }
}