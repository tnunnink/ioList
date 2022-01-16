using System.Collections.ObjectModel;
using ioList.Module.LoadFile.Model;
using ioList.Module.LoadFile.Services;
using Prism.Commands;
using Prism.Mvvm;

namespace ioList.Module.LoadFile.ViewModels
{
    public class RecentFilesViewModel : BindableBase
    {
        private DelegateCommand _loadFileCommand;

        /// <summary>
        /// Design Time Constructor
        /// </summary>
        public RecentFilesViewModel()
        {
            RecentFiles = new ObservableCollection<RecentFile>
            {
                new("MyController.L5X"),
                new("SomeExportFile.L5X"),
                new("ModuleExport.L5X"),
                new("MyProject.L5X"),
            };
        }
        
        public RecentFilesViewModel(IRecentFileDataService dataService)
        {
            var files = dataService.GetAll();
            RecentFiles = new ObservableCollection<RecentFile>(files);
        }

        public ObservableCollection<RecentFile> RecentFiles { get; }


        public DelegateCommand LoadFileCommand =>
            _loadFileCommand ??= new DelegateCommand(ExecuteLoadFileCommand);

        private void ExecuteLoadFileCommand()
        {
            
        }
    }
}