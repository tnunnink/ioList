using System.Collections.ObjectModel;
using System.Linq;
using ioList.Module.IoSelection.Observers;
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
        private readonly ICardDataService _cardDataService;
        private ObservableCollection<ModuleObserver> _modules;


        public IoTreeViewModel()
        {
            _cardDataService = new FakeCardDataService();
            _cardDataService.Load(string.Empty);
            Load();
        }

        public IoTreeViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _cardDataService = new FakeCardDataService();
        }

        public ObservableCollection<ModuleObserver> Modules
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
            
            _cardDataService.Load(fileName);
            Load();
        }

        private void Load()
        {
            var modules = _cardDataService.GetAll();

            Modules ??= new ObservableCollection<ModuleObserver>();
            Modules.Clear();
            Modules.AddRange(modules.Select(m => new ModuleObserver(m)));
        }

        public bool IsNavigationTarget(NavigationContext navigationContext) => true;

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }
    }
}