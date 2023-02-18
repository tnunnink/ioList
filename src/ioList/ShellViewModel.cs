using System.Windows;
using CoreWPF.Mvvm;
using CoreWPF.Prism;
using ioList.Composites;
using ioList.Core;
using ioList.Events;
using ioList.Shared;
using ioList.Shared.Services;
using NLog;
using Prism.Events;
using Prism.Regions;

namespace ioList
{
    public class ShellViewModel : ViewModelBase, IRegionManagerAware
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
        private readonly IEventAggregator _eventAggregator;
        private readonly IScopedShellCreator _shellCreator;
        private Project _project;
        private ResizeMode _resizeMode = ResizeMode.CanMinimize;

        public ShellViewModel(IEventAggregator eventAggregator, IScopedShellCreator shellCreator)
        {
            _eventAggregator = eventAggregator;
            _shellCreator = shellCreator;

            _eventAggregator.GetEvent<OpenProjectEvent>().Subscribe(LaunchProject);
        }

        public ResizeMode ResizeMode
        {
            get => _resizeMode;
            private set => SetProperty(ref _resizeMode, value);
        }

        private void LaunchProject(Project project)
        {
            _project = project;
            ResizeMode = ResizeMode.CanResize;
            Title = _project.Name;
            
            RegionManager?.RequestNavigate(Regions.ContentRegion, nameof(ProjectView));
        }
        
        public IRegionManager RegionManager { get; set; }
    }
}