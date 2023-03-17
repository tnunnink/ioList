using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MaterialDesignThemes.Wpf;
using Squirrel;
using Squirrel.Sources;

namespace ioList.ViewModels
{
    public partial class FooterViewModel : ObservableObject
    {
        private readonly TaskNotifier _loadTask;
        private readonly EventLog _eventLog;


        public FooterViewModel(ISnackbarMessageQueue messageQueue)
        {
            _eventLog = new EventLog("Application");
            _messageQueue = messageQueue;
            LoadTask = CheckForUpdates();
        }

        [ObservableProperty] //
        private ISnackbarMessageQueue _messageQueue;

        [ObservableProperty] // 
        private string _updateText;

        [ObservableProperty] //
        [NotifyCanExecuteChangedFor(nameof(PerformUpdateCommand))]
        private bool _updateAvailable = false;


        private Task LoadTask
        {
            get => _loadTask;
            init => SetPropertyAndNotifyOnCompletion(ref _loadTask, value);
        }

        [RelayCommand]
        private async Task CheckForUpdates()
        {
            try
            {
                using var manager = new UpdateManager(new GithubSource(App.ReadMeUrl, string.Empty, false));

                if (!manager.IsInstalledApp) return;

                var updates = await manager.CheckForUpdate();

                UpdateAvailable = updates.ReleasesToApply.Count > 0;

                if (!UpdateAvailable) return;

                var latest = updates.ReleasesToApply.MaxBy(r => r.Version)?.Version;
                UpdateText = $"Update to version {latest}";
            }
            catch (Exception e)
            {
                _eventLog.WriteEntry($"Failed to perform check for updates with error '{e.Message}'.",
                    EventLogEntryType.Error);

                MessageQueue.Enqueue("Unable to contact Github to find new updates.");
            }
        }

        [RelayCommand(CanExecute = nameof(CanExecuteUpdateCommand))]
        private async Task PerformUpdate()
        {
            _eventLog.WriteEntry("Starting application update.", EventLogEntryType.Information);
            
            UpdateText = "Updating application. Once complete the app will restart.";

            try
            {
                using var manager = new UpdateManager(new GithubSource(App.ReadMeUrl, string.Empty, false));
                var release = await manager.UpdateApp();

                if (release != null)
                    UpdateManager.RestartApp();
            }
            catch (Exception e)
            {
                _eventLog.WriteEntry($"Failed to perform application update with error '{e.Message}'.",
                    EventLogEntryType.Error);

                MessageQueue.Enqueue("Application update failed. Check the Windows Event Log for more info.");
            }
        }

        private bool CanExecuteUpdateCommand() => UpdateAvailable;

        [RelayCommand]
        private void LaunchSite(string url)
        {
            try
            {
                Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
            }
            catch (Exception e)
            {
                _eventLog.WriteEntry($"Unable to open site {url} due to error '{e.Message}'.", EventLogEntryType.Error);
                MessageQueue.Enqueue($"Unable to open site {url}");
            }
        }
    }
}