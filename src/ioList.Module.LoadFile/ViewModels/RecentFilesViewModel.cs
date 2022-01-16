using System.Collections.ObjectModel;
using System.Linq;
using ioList.Events;
using ioList.Module.LoadFile.Model;
using ioList.Module.LoadFile.Services;
using ioList.Module.LoadFile.Services.Fakes;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;

// ReSharper disable UnusedMember.Global used by IOC container

namespace ioList.Module.LoadFile.ViewModels
{
    public class RecentFilesViewModel : BindableBase
    {
        private readonly IRecentFileDataService _recentFileDataService;
        private readonly IEventAggregator _eventAggregator;
        private RecentFile _selectedRecentFile;
        private DelegateCommand _clearAllCommand;
        private bool _hasRecentFiles;


        /// <summary>
        /// Design Time Constructor
        /// </summary>
        public RecentFilesViewModel()
        {
            _recentFileDataService = new FakeRecentFileDataService();
            Load();
        }

        public RecentFilesViewModel(IRecentFileDataService recentFileDataService, IEventAggregator eventAggregator)
        {
            _recentFileDataService = recentFileDataService;
            _eventAggregator = eventAggregator;
            Load();
        }

        public bool HasRecentFiles
        {
            get => _hasRecentFiles;
            set => SetProperty(ref _hasRecentFiles, value);
        }

        public ObservableCollection<RecentFile> RecentFiles { get; private set; }
        
        public RecentFile SelectedRecentFile
        {
            get => _selectedRecentFile;
            set
            {
                SetProperty(ref _selectedRecentFile, value);
                LoadFile(_selectedRecentFile.FullPath);
            }
        }


        public DelegateCommand ClearAllCommand =>
            _clearAllCommand ??= new DelegateCommand(ExecuteClearAllCommand);

        private DelegateCommand _clearNonExistingCommand;

        public DelegateCommand ClearNonExistingCommand =>
            _clearNonExistingCommand ??= new DelegateCommand(ExecuteClearNonExistingCommand);

        private DelegateCommand<RecentFile> _removeFileCommand;

        public DelegateCommand<RecentFile> RemoveFileCommand =>
            _removeFileCommand ??= new DelegateCommand<RecentFile>(ExecuteRemoveFileCommand);


        private void Load()
        {
            var files = _recentFileDataService.GetAll();

            RecentFiles ??= new ObservableCollection<RecentFile>();
            RecentFiles.Clear();
            RecentFiles.AddRange(files);

            HasRecentFiles = RecentFiles.Any();
        }

        private void ExecuteClearAllCommand()
        {
            RecentFiles.Clear();
            _recentFileDataService.Clear();
            HasRecentFiles = RecentFiles.Any();
        }

        private void ExecuteClearNonExistingCommand()
        {
            var nonExisting = RecentFiles.Where(f => !f.Exists);

            foreach (var file in nonExisting)
                _recentFileDataService.Remove(file);
            
            Load();
        }

        private void ExecuteRemoveFileCommand(RecentFile recentFile)
        {
            if (recentFile is null)
                return;
            
            _recentFileDataService.Remove(recentFile);
            
            Load();
        }

        private void LoadFile(string fillPath)
        {
            if (fillPath is null) 
                return;

            _eventAggregator.GetEvent<LoadFileEvent>().Publish(fillPath);
        }
    }
}