using System.Collections.Generic;
using System.Collections.ObjectModel;
using ioList.Module.IoSelection.Model;
using ioList.Module.IoSelection.Services;
using ioList.Module.IoSelection.Services.Fakes;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;

namespace ioList.Module.IoSelection.ViewModels
{
    public class IoTreeViewModel : BindableBase, INavigationAware
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IModuleDataService _moduleDataService;
        private ObservableCollection<LogixModule> _modules;


        public IoTreeViewModel()
        {
            _moduleDataService = new FakeModuleDataService();
            _moduleDataService.Load(string.Empty);
            Load();
        }

        public IoTreeViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _moduleDataService = new FakeModuleDataService();
        }

        public ObservableCollection<LogixModule> Modules
        {
            get => _modules;
            private set => SetProperty(ref _modules, value);
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            var fileName = navigationContext.Parameters.GetValue<string>("FileName");

            if (string.IsNullOrEmpty(fileName))
            {
                //issue warning?
            }
            
            _moduleDataService.Load(fileName);
            Load();
        }

        private void Load()
        {
            var modules = _moduleDataService.GetAll();

            Modules ??= new ObservableCollection<LogixModule>();
            Modules.Clear();
            Modules.AddRange(modules);
        }

        public bool IsNavigationTarget(NavigationContext navigationContext) => true;

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }
    }
}