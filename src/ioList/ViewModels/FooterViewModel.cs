using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
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

        public FooterViewModel(ISnackbarMessageQueue messageQueue)
        {
            _messageQueue = messageQueue;
            LoadTask = CheckForUpdates();
        }

        [ObservableProperty] private ISnackbarMessageQueue _messageQueue;


        #region PropertyRegion

        [ObservableProperty] 
        private string _updateText;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(PerformUpdateCommand))]
        private bool _updateAvailable = false;

        #endregion


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
                using var manager = new UpdateManager(new GithubSource(App.RepositoryUrl, string.Empty, false));

                if (!manager.IsInstalledApp) return;
                
                var updates = await manager.CheckForUpdate();
                
                UpdateAvailable = updates.ReleasesToApply.Count > 0;

                if (!UpdateAvailable) return;

                var latest = updates.ReleasesToApply.MaxBy(r => r.Version)?.Version;
                UpdateText = $"Update to version {latest}";
            }
            catch (Exception e)
            {
                //I think we probably just log this?  perhaps inform user.
                MessageQueue.Enqueue("Unable to contact Github to find new updates...");
            }
        }

        [RelayCommand(CanExecute = nameof(CanExecuteUpdateCommand))]
        private async Task PerformUpdate()
        {
            try
            {
                using var manager = new UpdateManager(new GithubSource(App.RepositoryUrl, string.Empty, false));
                var release = await manager.UpdateApp();
                
                if (release == null) return;
                
                //todo perhaps prompt to restart
                UpdateManager.RestartApp();
            }
            catch (Exception e)
            {
                MessageQueue.Enqueue("Failed performing application update. You hate to see it...");
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
                MessageQueue.Enqueue($"Unable to open site {url}");
            }
        }
    }
}