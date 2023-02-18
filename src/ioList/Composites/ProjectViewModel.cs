using CoreWPF.Mvvm;
using ioList.Core;
using Prism.Regions;

namespace ioList.Composites
{
    public class ProjectViewModel : NavigationViewModelBase
    {
        private string _test;

        public string Test
        {
            get => _test;
            set => SetProperty(ref _test, value);
        }
        
        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            var project = navigationContext.Parameters.GetValue<Project>("Project");

            Test = project.Name;
        }
    }
}