using System.Collections.Generic;
using CoreTools.WPF.Mvvm;
using EntityObserver;
using ioList.Domain;
using ioList.Module.Settings.Views;
using ioList.Observers;
using Prism.Commands;
using Prism.Regions;
using Prism.Services.Dialogs;

namespace ioList.Module.Settings.ViewModels
{
    public class NewListViewModel : DialogViewModelBase
    {
        private static readonly List<string> SettingsViews = new()
        {
            nameof(FileSettingsView),
            nameof(ModuleSettingsView),
            nameof(FilterSettingsView),
            nameof(BufferSettingsView),
            nameof(ScalingSettingsView),
        };

        private readonly ListObserver _list;
        private DelegateCommand _nextCommand;
        private DelegateCommand _backCommand;
        private DelegateCommand _createCommand;
        private int _index;
        private bool _initialStep;
        private bool _finalStep;


        public NewListViewModel()
        {
            Title = "New List";
        }

        public ListObserver List
        {
            get => _list;
            init => SetProperty(ref _list, value);
        }

        private int Index
        {
            get => _index;
            set
            {
                InitialStep = value == 0;
                FinalStep = value == SettingsViews.Count - 1;
                SetProperty(ref _index, value);
            }
        }

        public bool InitialStep
        {
            get => _initialStep;
            set => SetProperty(ref _initialStep, value);
        }
        
        public bool FinalStep
        {
            get => _finalStep;
            set => SetProperty(ref _finalStep, value);
        }

        public DelegateCommand NextCommand =>
            _nextCommand ??= new DelegateCommand(ExecuteNextCommand, CanExecuteNextCommand)
                .ObservesProperty(() => List.IsChanged);
        
        public DelegateCommand BackCommand =>
            _backCommand ??= new DelegateCommand(ExecuteBackCommand, CanExecuteBackCommand)
                .ObservesProperty(() => Index);

        public DelegateCommand CreateCommand =>
            _createCommand ??= new DelegateCommand(ExecuteCreateCommand);

        private void ExecuteCreateCommand()
        {
            List.Validate(ValidationOption.All);

            if (!List.HasErrors)
            {
                //todo create list
            }
        }

        public override void OnDialogOpened(IDialogParameters parameters)
        {
            Index = 0;
            NavigateSetting(Index);
        }

        private void ExecuteBackCommand() => NavigateSetting(--Index);

        private bool CanExecuteBackCommand() => !InitialStep;

        private void ExecuteNextCommand()
        {
            List.Validate(ValidationOption.All);

            if (!List.HasErrors)
                NavigateSetting(++Index);
        } 

        private bool CanExecuteNextCommand() =>
            !string.IsNullOrEmpty(List.Name) 
            && !string.IsNullOrEmpty(List.Directory) 
            && !string.IsNullOrEmpty(List.SourceFile)
            && !List.HasErrors
            && Index < SettingsViews.Count;

        private void NavigateSetting(int index)
        {
            var viewName = SettingsViews[index];

            var parameters = new NavigationParameters { { "Option", List } };

            RegionManager?.RequestNavigate("ContentRegion", viewName, parameters);
        }
    }
}