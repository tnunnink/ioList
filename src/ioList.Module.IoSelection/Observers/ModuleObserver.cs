using EntityObserver;

namespace ioList.Module.IoSelection.Observers
{
    public class ModuleObserver : Observer<Domain.Module>
    {
        public ModuleObserver(Domain.Module model) : base(model)
        {
        }

        public string Name
        {
            get => GetValue<string>();
            set => SetValue(value);
        }

        public string CatalogNumber
        {
            get => GetValue<string>();
            set => SetValue(value);
        }
        
    }
}