using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using ioList.Events;
using ioList.Module.LoadFile.Observers;
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
        private RecentFileObserver _selectedFileViewModel;
        private DelegateCommand _clearAllCommand;
        private bool _hasRecentFiles;


        /// <summary>
        /// Design Time Constructor
        /// </summary>
        public RecentFilesViewModel()
        {
            if (DesignerProperties.GetIsInDesignMode(new DependencyObject())) return;

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

        public ObservableCollection<RecentFileObserver> RecentFiles { get; private set; }

        public RecentFileObserver SelectedFileViewModel
        {
            get => _selectedFileViewModel;
            set
            {
                SetProperty(ref _selectedFileViewModel, value);
                LoadFile(_selectedFileViewModel.FullPath);
            }
        }


        public DelegateCommand ClearAllCommand =>
            _clearAllCommand ??= new DelegateCommand(ExecuteClearAllCommand);

        private DelegateCommand _clearNonExistingCommand;

        public DelegateCommand ClearNonExistingCommand =>
            _clearNonExistingCommand ??= new DelegateCommand(ExecuteClearNonExistingCommand);

        private DelegateCommand<RecentFileObserver> _removeFileCommand;

        public DelegateCommand<RecentFileObserver> RemoveFileCommand =>
            _removeFileCommand ??= new DelegateCommand<RecentFileObserver>(ExecuteRemoveFileCommand);


        private void Load()
        {
            var files = _recentFileDataService.GetAll();

            RecentFiles ??= new ObservableCollection<RecentFileObserver>();
            RecentFiles.Clear();
            RecentFiles.AddRange(files.Select(f => new RecentFileObserver(f)));

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
                _recentFileDataService.Remove(file.Entity);

            Load();
        }

        private void ExecuteRemoveFileCommand(RecentFileObserver fileViewModel)
        {
            if (fileViewModel is null)
                return;

            _recentFileDataService.Remove(fileViewModel.Entity);

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