using System.Windows;
using CoreWPF.Mvvm;
using CoreWPF.Prism;
using ioList.Entities;
using ioList.Events;
using ioList.Shared.Services;
using Prism.Events;
using Prism.Regions;

namespace ioList
{
    public class ShellViewModel : ViewModelBase, IRegionManagerAware
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IScopedShellCreator _shellCreator;
        private ResizeMode _resizeMode = ResizeMode.CanMinimize;

        public ShellViewModel(IEventAggregator eventAggregator, IScopedShellCreator shellCreator)
        {
            _eventAggregator = eventAggregator;
            _shellCreator = shellCreator;

            _eventAggregator.GetEvent<LaunchProjectEvent>().Subscribe(LaunchProject);
        }

        private void LaunchProject(ProjectFile obj)
        {
            //todo close out the startup view.
            //kick off the project creation and migration.
            //start navigation of views into content region.
            //set data store recent projects to the new project file.
            //set Title to the project name and location?.
        }

        public IRegionManager RegionManager { get; set; }


        public ResizeMode ResizeMode
        {
            get => _resizeMode;
            set => SetProperty(ref _resizeMode, value);
        }
    }
}