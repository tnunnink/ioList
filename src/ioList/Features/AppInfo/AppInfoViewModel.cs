using System;
using System.Threading.Tasks;
using CoreTools.WPF.Mvvm;
using NLog;
using Prism.Commands;
using Prism.Regions;
using Squirrel;

namespace ioList.Features.AppInfo
{
    public class AppInfoViewModel : NavigationViewModelBase
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
        private const string GitRepo = "https://github.com/tnunnink/ioList";
        private UpdateManager _manager;
        private bool _updatesAvailable;
        private DelegateCommand _updateCommand;

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            LoadAsync().Await(OnLoadComplete, OnLoadError);
        }

        private string _versionText;

        public string VersionText
        {
            get => _versionText;
            set => SetProperty(ref _versionText, value);
        }

        public bool UpdatesAvailable
        {
            get => _updatesAvailable;
            set => SetProperty(ref _updatesAvailable, value);
        }

        public DelegateCommand UpdateCommand =>
            _updateCommand ??= new DelegateCommand(ExecuteUpdateCommand, CanExecuteUpdateCommand);

        private void ExecuteUpdateCommand()
        {
            _manager.UpdateApp().Await(OnUpdateComplete, OnUpdateFailed);
        }

        private void OnUpdateFailed(Exception obj)
        {
            Logger.Error(obj, "Failed to update application");
        }

        private void OnUpdateComplete(ReleaseEntry obj)
        {
            Logger.Info($"Application Updates to version {obj.Version}");
        }

        private bool CanExecuteUpdateCommand() => UpdatesAvailable;

        protected override async Task LoadAsync()
        {
            _manager = await UpdateManager.GitHubUpdateManager(GitRepo);
            var updates = await _manager.CheckForUpdate();
            UpdatesAvailable = updates.ReleasesToApply.Count > 0;
        }

        protected override void OnLoadError(Exception ex)
        {
            Logger.Error(ex, "Unable to connect to update manager");
        }

        public override void Destroy()
        {
            _manager.Dispose();
        }
    }
}