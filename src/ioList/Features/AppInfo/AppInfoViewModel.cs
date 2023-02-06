using System.Threading.Tasks;
using CoreTools.WPF.Mvvm;
using Prism.Regions;
using Squirrel;

namespace ioList.Features.AppInfo
{
    public class AppInfoViewModel : NavigationViewModelBase
    {
        private const string GitRepo = "https://github.com/tnunnink/ioList";
        private UpdateManager _manager;

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            LoadAsync().Await(OnLoadComplete, OnLoadError);
        }

        protected override async Task LoadAsync()
        {
            _manager = await UpdateManager.GitHubUpdateManager(GitRepo);
            
            
        }

        public override void Destroy()
        {
            _manager.Dispose();
        }
    }
}