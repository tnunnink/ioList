using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ioList.Entities;
using ioList.Generation;
using ioList.Views;
using MaterialDesignThemes.Wpf;
using Squirrel;
using Squirrel.Sources;

namespace ioList.ViewModels
{
    public partial class FooterViewModel : ObservableObject
    {
        private readonly TaskNotifier _loadTask;

        public FooterViewModel(ISnackbarMessageQueue messageQueue)
        {
            _messageQueue = messageQueue;
            LoadTask = CheckForUpdates();
        }

        [ObservableProperty] private ISnackbarMessageQueue _messageQueue;

        [ObservableProperty] private string _updateText;

        [ObservableProperty] [NotifyCanExecuteChangedFor(nameof(PerformUpdateCommand))]
        private bool _updateAvailable;


        private Task LoadTask
        {
            init => SetPropertyAndNotifyOnCompletion(ref _loadTask, value);
        }

        [RelayCommand]
        private async Task CheckForUpdates()
        {
            using var log = new EventLog("Application", Environment.MachineName, "Application");
            
            try
            {
                log.WriteEntry($"Connecting to repository at {App.RepositoryUrl} to check for updates.",
                    EventLogEntryType.Information);

                using var manager = new UpdateManager(new GithubSource(App.RepositoryUrl, string.Empty, false));

                if (!manager.IsInstalledApp) return;

                log.WriteEntry("Performing checking for updates.", EventLogEntryType.Information);

                var updates = await manager.CheckForUpdate();

                if (updates is null) return;

                log.WriteEntry($"{updates.ReleasesToApply.Count} New Releases Found.",
                    EventLogEntryType.Information);

                UpdateAvailable = updates.ReleasesToApply.Count > 0;

                if (!UpdateAvailable) return;

                var latest = updates.ReleasesToApply.MaxBy(r => r.Version)?.Version;
                UpdateText = $"Update to version {latest}";
            }
            catch (Exception e)
            {
                log.WriteEntry($"Failed to perform check for updates with error '{e.Message}'.",
                    EventLogEntryType.Error);

                MessageQueue.Enqueue("Unable to contact Github to find new updates.");
            }
        }

        [RelayCommand(CanExecute = nameof(CanExecuteUpdateCommand))]
        private async Task PerformUpdate()
        {
            using var log = new EventLog("Application", Environment.MachineName, "Application");
            
            log.WriteEntry("Starting application update.", EventLogEntryType.Information);

            UpdateText = "Updating application. Once complete the app will restart.";

            try
            {
                using var manager = new UpdateManager(new GithubSource(App.RepositoryUrl, string.Empty, false));
                var release = await manager.UpdateApp();

                if (release != null)
                    UpdateManager.RestartApp();
            }
            catch (Exception e)
            {
                log.WriteEntry($"Failed to perform application update with error '{e.Message}'.",
                    EventLogEntryType.Error);

                MessageQueue.Enqueue("Application update failed. Check the Windows Event Log for more info.");
            }
        }

        private bool CanExecuteUpdateCommand() => UpdateAvailable;
        
        [RelayCommand]
        private static async Task OpenConfiguration()
        {
            var config = new GeneratorConfig();
            var viewModel = new ConfigurationViewModel(config);
            var view = new ConfigurationView { DataContext = viewModel };

            var save = await DialogHost.Show(view, "ShellHost");

            /*config.Save();*/
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
                MessageQueue.Enqueue($"Unable to open site {url}");
            }
        }
    }
}