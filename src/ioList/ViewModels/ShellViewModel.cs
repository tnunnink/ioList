using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GongSolutions.Wpf.DragDrop;
using ioList.Model;
using ioList.Shared;
using ioList.Views;
using MaterialDesignThemes.Wpf;
using Ookii.Dialogs.Wpf;
using Squirrel;
using Squirrel.Sources;

namespace ioList.ViewModels
{
    public partial class ShellViewModel : ObservableValidator, IDropTarget
    {
        private readonly GeneratorOptions _options;
        private readonly ISnackbarMessageQueue _messageQueue;

        public ShellViewModel(GeneratorOptions options, ISnackbarMessageQueue messageQueue)
        {
            _options = options;
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
        [CustomValidation(typeof(ShellViewModel), nameof(ValidateFileName))]
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
            LaunchSite("https://github.com/tnunnink/ioList/issues/new");
            ViewIndex = 0;
        }

        [RelayCommand]
        private async Task OpenOptions()
        {
            var viewModel = new OptionsViewModel();
            var view = new OptionsView { DataContext = viewModel };

            var save = await DialogHost.Show(view, "ShellHost");

            if (save is true)
            {
                _options.Save();
            }
        }

        [RelayCommand(CanExecute = nameof(CanGenerate))]
        private void Generate()
        {
            ViewIndex++;

            Task.Run(() =>
            {
                using var log = new EventLog("Application", Environment.MachineName, "Application");

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
                    log.WriteEntry($"Processing failed with error message '{e.Message}'.",
                        EventLogEntryType.Error);
                    ErrorMessage = e.Message;
                    ViewIndex = 4;
                }
            });
        }

        public void DragOver(IDropInfo dropInfo)
        {
            var files = ((DataObject)dropInfo.Data).GetFileDropList().Cast<string>().ToList();

            var info = files.Count == 1 ? new FileInfo(files[0]) : null;

            dropInfo.Effects = info is not null && info.Exists && info.Extension == ".L5X"
                ? DragDropEffects.Copy
                : DragDropEffects.None;
        }

        public void Drop(IDropInfo dropInfo)
        {
            var files = ((DataObject)dropInfo.Data).GetFileDropList().Cast<string>().ToList();

            var info = files.Count == 1 ? new FileInfo(files[0]) : null;

            if (info is null || !info.Exists || info.Extension != ".L5X")
                return;

            SourceFile = info.FullName;
            DestinationName = $"{Path.GetFileNameWithoutExtension(info.FullName)} IO List";
            DestinationLocation = info.DirectoryName ?? string.Empty;

            //Go to the entry view if not already there.
            ViewIndex = 1;
        }

        private bool CanGenerate() => !string.IsNullOrWhiteSpace(SourceFile)
                                      && !string.IsNullOrWhiteSpace(DestinationName)
                                      && !string.IsNullOrWhiteSpace(DestinationLocation)
                                      && !HasErrors;

        public static ValidationResult ValidateFileName(string value, ValidationContext context)
        {
            return value.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0
                ? new ValidationResult($"The name {value} is not a valid file name.")
                : ValidationResult.Success;
        }

        public static ValidationResult ValidatePath(string value, ValidationContext context)
        {
            return value.IndexOfAny(Path.GetInvalidPathChars()) >= 0
                ? new ValidationResult($"The path {value} is not a valid directory.")
                : ValidationResult.Success;
        }

        private void GetVersion()
        {
            using var log = new EventLog("Application", Environment.MachineName, "Application");

            try
            {
                log.WriteEntry($"Connecting to repository at {App.RepositoryUrl} to get current version.",
                    EventLogEntryType.Information);

                using var manager = new UpdateManager(new GithubSource(App.RepositoryUrl, string.Empty, false));
                var installedVersion = manager.CurrentlyInstalledVersion();

                log.WriteEntry($"Current installed version: {installedVersion}", EventLogEntryType.Information);

                Version = installedVersion is not null ? installedVersion.ToString() : string.Empty;
            }
            catch (Exception e)
            {
                log.WriteEntry($"Failed to get current version from Github with error message '{e.Message}'.",
                    EventLogEntryType.Error);
            }
        }

        [RelayCommand]
        private void LaunchSite(string url)
        {
            using var log = new EventLog("Application", Environment.MachineName, "Application");

            try
            {
                Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
            }
            catch (Exception e)
            {
                log.WriteEntry($"Unable to open site {url} due to error '{e.Message}'.", EventLogEntryType.Error);
                _messageQueue.Enqueue($"Unable to open site {url}");
            }
        }
    }
}