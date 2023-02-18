using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CoreWPF.Mvvm;
using NLog;
using Prism.Commands;
using Squirrel;

namespace ioList.Features.AppInfo
{
    public class AppInfoViewModel : ViewModelBase
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
        private DelegateCommand _launchRepository;
        private DelegateCommand _launchIssues;
        private DelegateCommand _launchPages;
        private DelegateCommand _updateCommand;
        private DelegateCommand _checkForUpdates;
        private string _versionText;
        private string _updateText;
        private bool _updateAvailable;
        private bool _checkingForUpdates;

        public AppInfoViewModel()
        {
            VersionText = "ioList 0.0.0";
            UpdateAvailable = false;
            ExecuteCheckForUpdates();
        }

        public string VersionText
        {
            get => _versionText;
            private set => SetProperty(ref _versionText, value);
        }

        public string UpdateText
        {
            get => _updateText;
            private set => SetProperty(ref _updateText, value);
        }

        public bool UpdateAvailable
        {
            get => _updateAvailable;
            private set => SetProperty(ref _updateAvailable, value);
        }

        public bool CheckingForUpdates
        {
            get => _checkingForUpdates;
            set => SetProperty(ref _checkingForUpdates, value);
        }

        public DelegateCommand LaunchRepository =>
            _launchRepository ??= new DelegateCommand(() => LaunchSite(Shared.Global.RepositoryUrl));

        public DelegateCommand LaunchIssues =>
            _launchIssues ??= new DelegateCommand(() => LaunchSite(Shared.Global.IssuesUrl));

        public DelegateCommand LaunchPages =>
            _launchPages ??= new DelegateCommand(() => LaunchSite(Shared.Global.IssuesUrl));

        public DelegateCommand CheckForUpdates =>
            _checkForUpdates ??= new DelegateCommand(ExecuteCheckForUpdates, () => !CheckingForUpdates)
                .ObservesProperty(() => CheckingForUpdates);

        public DelegateCommand UpdateCommand =>
            _updateCommand ??= new DelegateCommand(ExecuteUpdateCommand, CanExecuteUpdateCommand);

        private void ExecuteCheckForUpdates()
        {
            Task.Run(async () =>
            {
                CheckingForUpdates = true;
                using var manager = await UpdateManager.GitHubUpdateManager(Shared.Global.RepositoryUrl);
                return await manager.CheckForUpdate();
            }).Await(OnCheckForUpdatesComplete,
                e =>
                {
                    Logger.Error(e, $"Unable to perform application update check for '{Shared.Global.RepositoryUrl}'.");
                    CheckingForUpdates = false;
                }, false);
        }

        private void OnCheckForUpdatesComplete(UpdateInfo updateInfo)
        {
            CheckingForUpdates = false;

            if (updateInfo is null)
                return;

            VersionText = $"ioList {updateInfo.CurrentlyInstalledVersion.Version}";

            UpdateAvailable = updateInfo.ReleasesToApply.Count > 0;

            if (!UpdateAvailable) return;
            var latest = updateInfo.ReleasesToApply.OrderBy(r => r.Version).LastOrDefault()?.Version;
            UpdateText = $"Update to version {latest}";
        }

        private void ExecuteUpdateCommand()
        {
            Task.Run(async () =>
            {
                using var manager = await UpdateManager.GitHubUpdateManager(Shared.Global.RepositoryUrl);
                return await manager.UpdateApp();
            }).Await(OnUpdateComplete,
                e => Logger.Error(e, $"Unable to update application from repository '{Shared.Global.RepositoryUrl}'."));
        }

        private bool CanExecuteUpdateCommand() => UpdateAvailable;

        private void OnUpdateComplete(ReleaseEntry entry)
        {
            Logger.Info($"Application updated to version {entry.Version}");
            VersionText = $"ioList {entry.Version}";
            UpdateAvailable = false;
        }

        private static void LaunchSite(string url)
        {
            try
            {
                Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
            }
            catch (Exception e)
            {
                Logger.Error(e, $"Unable to open site {url}");
            }
        }
    }
}