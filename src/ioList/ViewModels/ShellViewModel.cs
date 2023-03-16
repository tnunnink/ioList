using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ioList.Shared;
using MaterialDesignThemes.Wpf;
using Ookii.Dialogs.Wpf;
using Squirrel;
using Squirrel.Sources;

namespace ioList.ViewModels
{
    public partial class ShellViewModel : ObservableValidator
    {
        private readonly ISnackbarMessageQueue _messageQueue;

        public ShellViewModel(ISnackbarMessageQueue messageQueue)
        {
            _messageQueue = messageQueue;
            GetVersion();
        }

        [ObservableProperty] private string _version;

        [ObservableProperty] private int _viewIndex;

        [ObservableProperty] [NotifyCanExecuteChangedFor(nameof(GenerateCommand))] [NotifyDataErrorInfo] [Required]
        private string _sourceFile = string.Empty;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(GenerateCommand))]
        [NotifyDataErrorInfo]
        [Required]
        [CustomValidation(typeof(ShellViewModel), nameof(ValidatePath))]
        private string _destinationName = string.Empty;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(GenerateCommand))]
        [NotifyDataErrorInfo]
        [Required]
        [CustomValidation(typeof(ShellViewModel), nameof(ValidatePath))]
        private string _destinationLocation = string.Empty;

        [ObservableProperty] private string _errorMessage;

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

            ViewIndex = 0;
        }

        [RelayCommand]
        private void TryAgain() => ViewIndex = 0;

        [RelayCommand]
        private void ReportIssue()
        {
            //todo what should this do...?
            ViewIndex = 0;
        }

        [RelayCommand(CanExecute = nameof(CanGenerate))]
        private void Generate()
        {
            ViewIndex++;

            Task.Run(() =>
            {
                try
                {
                    if (!Directory.Exists(DestinationLocation))
                        Directory.CreateDirectory(DestinationLocation);
                    
                    var destination = Path.Combine(DestinationLocation, $"{DestinationName}.csv");
                    
                    Generator.Generate(SourceFile, destination);
                    
                    ViewIndex++;
                }
                catch (Exception e)
                {
                    ErrorMessage = e.Message;
                    ViewIndex = 4;
                }
            });
        }

        private bool CanGenerate() => !string.IsNullOrWhiteSpace(SourceFile)
                                      && !string.IsNullOrWhiteSpace(DestinationName)
                                      && !string.IsNullOrWhiteSpace(DestinationLocation)
                                      && !HasErrors;

        public static ValidationResult ValidatePath(string value, ValidationContext context)
        {
            return value.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0
                ? new ValidationResult($"The name {value} is not a valid file name.")
                : ValidationResult.Success;
        }

        private void GetVersion()
        {
            try
            {
                using var manager = new UpdateManager(new GithubSource(App.RepositoryUrl, string.Empty, false));
                var installedVersion = manager.CurrentlyInstalledVersion();
                Version = installedVersion is not null ? installedVersion.ToString() : string.Empty;
            }
            catch (Exception e)
            {
                //todo log
                Console.WriteLine(e);
            }
        }
    }
}