using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CoreWPF.Mvvm;
using CoreWPF.Services;
using ioList.Core;
using ioList.Events;
using ioList.Shared;
using NLog;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;

namespace ioList.Features.Startup
{
    public class StartupViewModel : NavigationViewModelBase
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
        private readonly IEventAggregator _eventAggregator;
        private bool _hasProjects;
        private ObservableCollection<Project> _projects;
        private Project _selectedProject;
        private string _projectName = "MyProject";
        private string _projectLocation = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        private DelegateCommand _newCommand;
        private DelegateCommand _openCommand;
        private DelegateCommand _createCommand;
        private DelegateCommand _backCommand;
        private DelegateCommand _selectLocationCommand;
        private readonly DefaultView _defaultView;
        private readonly OpenProjectView _openProjectView;
        private readonly NewProjectView _newProjectView;

        public StartupViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _defaultView = new DefaultView();
            _openProjectView = new OpenProjectView();
            _newProjectView = new NewProjectView();
        }

        public ObservableCollection<Project> Projects
        {
            get => _projects;
            private set => SetProperty(ref _projects, value);
        }

        public Project SelectedProject
        {
            get => _selectedProject;
            set
            {
                SetProperty(ref _selectedProject, value);
                LaunchProject(_selectedProject);
            }
        }

        public string ProjectName
        {
            get => _projectName;
            set => SetProperty(ref _projectName, value);
        }

        public string ProjectLocation
        {
            get => _projectLocation;
            private set => SetProperty(ref _projectLocation, value);
        }

        public DelegateCommand NewCommand =>
            _newCommand ??= new DelegateCommand(NavigateNewProjectView);

        public DelegateCommand OpenCommand =>
            _openCommand ??= new DelegateCommand(ExecuteOpenCommand);

        public DelegateCommand SelectLocationCommand =>
            _selectLocationCommand ??= new DelegateCommand(ExecuteSelectLocationCommand);

        public DelegateCommand CreateCommand =>
            _createCommand ??= new DelegateCommand(ExecuteCreateCommand, CanExecuteCreateCommand)
                .ObservesProperty(() => ProjectName)
                .ObservesProperty(() => ProjectLocation);

        public DelegateCommand BackCommand =>
            _backCommand ??= new DelegateCommand(NavigateStartupView);

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            RegionManager?.Regions[Regions.PageRegion].Add(_defaultView);
            RegionManager?.Regions[Regions.PageRegion].Add(_openProjectView);
            RegionManager?.Regions[Regions.PageRegion].Add(_newProjectView);

            Projects = new ObservableCollection<Project>(LoadProjects());
            _hasProjects = Projects.Count > 0;

            NavigateStartupView();
        }

        private void NavigateNewProjectView()
        {
            RegionManager?.Regions[Regions.PageRegion].Activate(_newProjectView);
        }

        private void NavigateStartupView()
        {
            if (_hasProjects)
            {
                RegionManager?.Regions[Regions.PageRegion].Activate(_openProjectView);
                return;
            }

            RegionManager?.Regions[Regions.PageRegion].Activate(_defaultView);
        }

        private void ExecuteOpenCommand()
        {
            var fileName = FileService.SelectFile();

            if (string.IsNullOrEmpty(fileName)) return;

            try
            {
                var project = new Project(fileName);
                LaunchProject(project);
            }
            catch (Exception e)
            {
                Logger.Error(e, $"Unable to open file {fileName}.");
            }
        }

        private void ExecuteSelectLocationCommand()
        {
            var folder = FileService.SelectFolder(o =>
            {
                o.UseDescriptionForTitle = true;
                o.Description = "Select project location";
                o.ShowNewFolderButton = true;
            });

            if (string.IsNullOrEmpty(folder)) return;

            ProjectLocation = folder;
        }

        private void ExecuteCreateCommand() => LaunchProject(new Project(ProjectName, ProjectLocation));

        private bool CanExecuteCreateCommand() =>
            !string.IsNullOrEmpty(ProjectName) && !string.IsNullOrEmpty(ProjectLocation);

        private static List<Project> LoadProjects()
        {
            try
            {
                var projects = Global.Data.Load<List<string>>("Projects.json");

                return projects is not null
                    ? projects.Select(p => new Project(p)).ToList()
                    : Enumerable.Empty<Project>().ToList();
            }
            catch (Exception e)
            {
                Logger.Error(e, "Unable to load recent project from local application data file.");
                return Enumerable.Empty<Project>().ToList();
            }
        }

        /// <summary>
        /// Event is subscribed to in the main shell view model. There is where the process of
        /// creating/migrating/connecting to the specified project is carried out.
        /// </summary>
        private void LaunchProject(Project project)
        {
            if (!project.Exists)
                CreateProject(project);

            MigrateProject(project);

            SaveProject(project);

            _eventAggregator.GetEvent<OpenProjectEvent>().Publish(project);
        }

        private static void CreateProject(Project project)
        {
            try
            {
                Logger.Info($"Creating project with name {project.Name}");
                project.Create();
            }
            catch (Exception e)
            {
                Logger.Error(e, "Filed to create project.");
            }
        }

        private static void MigrateProject(Project project)
        {
            try
            {
                Logger.Info($"Migrating {project.Name} to latest version.");
                project.Migrate();
            }
            catch (Exception e)
            {
                Logger.Error(e, $"Failed to migrate project {project.Name}");
            }
        }

        private static void SaveProject(Project project)
        {
            try
            {
                Logger.Info("Persisting project details to local data store.");
                var recent = Global.Data.Load<List<string>>("Projects.json") ?? new List<string>();
                if (recent.Contains(project.FileName)) return;
                recent.Add(project.FileName);
                Global.Data.Save(recent, "Projects.json");
                
            }
            catch (Exception e)
            {
                Logger.Error(e, "Filed to persist project to local data file.");
            }
        }
    }
}