using System.Collections.ObjectModel;
using CoreTools.WPF.Mvvm;
using ioList.Entities;
using Prism.Regions;

namespace ioList.Features.Startup.RecentProjects
{
    public class RecentProjectsViewModel : NavigationViewModelBase
    {
        private readonly RecentProjectsStore _store;

        public RecentProjectsViewModel()
        {
            _store = new RecentProjectsStore();
        }
        
        private ObservableCollection<ProjectFile> _projects;

        public ObservableCollection<ProjectFile> Projects
        {
            get => _projects;
            set => SetProperty(ref _projects, value);
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);

            Load();
        }

        protected override void Load()
        {
            Projects = new ObservableCollection<ProjectFile>(_store.GetProjects());
        }
    }
}