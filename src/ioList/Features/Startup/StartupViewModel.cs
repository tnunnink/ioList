using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CoreWPF.Mvvm;
using CoreWPF.Services;
using GongSolutions.Wpf.DragDrop;
using ioList.Entities;
using ioList.Events;
using ioList.Observers;
using ioList.Shared;
using NLog;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;

namespace ioList.Features.Startup
{
    public class StartupViewModel : NavigationViewModelBase, IDropTarget
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
        private readonly IEventAggregator _eventAggregator;
        private bool _hasProjects;
        private ObservableCollection<ProjectFileObserver> _projects;
        private ProjectFileObserver _project;
        private DelegateCommand _newCommand;
        private DelegateCommand _openCommand;
        private DelegateCommand _createCommand;


        public StartupViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            /*
            _startupSelectionView = new StartupSelectionView();
            _newProjectView = new NewProjectView();*/
        }

        public bool HasProjects
        {
            get => _hasProjects;
            set => SetProperty(ref _hasProjects, value);
        }

        public ObservableCollection<ProjectFileObserver> Projects
        {
            get => _projects;
            private set => SetProperty(ref _projects, value);
        }

        public ProjectFileObserver Project
        {
            get => _project;
            set => SetProperty(ref _project, value);
        }

        public DelegateCommand NewCommand =>
            _newCommand ??= new DelegateCommand(() => NavigateProjectView(new ProjectFileObserver()));

        public DelegateCommand OpenCommand =>
            _openCommand ??= new DelegateCommand(ExecuteOpenCommand);

        public DelegateCommand CreateCommand =>
            _createCommand ??= new DelegateCommand(LaunchProject, () => Project is not null && !Project.HasErrors)
                .ObservesProperty(() => Project.HasErrors);

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            var startupSelectionView = new StartupSelectionView { DataContext = this };
            RegionManager?.Regions[Regions.PageRegion].Add(startupSelectionView);

            Task.Run(() =>
            {
                Loading = true;

                var projects = Application.Data.Load<List<ProjectFile>>("Projects.json");

                Projects = projects is not null
                    ? new ObservableCollection<ProjectFileObserver>(
                        projects.Select(p => new ProjectFileObserver(p)))
                    : new ObservableCollection<ProjectFileObserver>();

                HasProjects = Projects.Count > 0;
            }).Await(() => Loading = false,
                e => Logger.Error(e, "Unable to load recent project from local application data file."));
        }

        public void DragOver(IDropInfo dropInfo)
        {
            throw new System.NotImplementedException();
        }

        public void Drop(IDropInfo dropInfo)
        {
            throw new System.NotImplementedException();
        }

        private void NavigateProjectView(ProjectFileObserver defaultProject)
        {
            Project = defaultProject;
            var view = new NewProjectView { DataContext = this };
            RegionManager?.Regions[Regions.PageRegion].Add(view);
            RegionManager?.Regions[Regions.PageRegion].Activate(view);
        }

        private void ExecuteOpenCommand()
        {
            var fileName = FileService.SelectFile();

            if (string.IsNullOrEmpty(fileName)) return;
            
            var projectFile = ProjectFile.FromFileName(fileName);
            Project = new ProjectFileObserver(projectFile);
            LaunchProject();
        }

        private void LaunchProject() =>
            _eventAggregator.GetEvent<LaunchProjectEvent>().Publish(Project.Entity);
    }
}