using ioList.Common.Naming;
using ioList.Module.Import.ViewModels;
using ioList.Module.Import.Views;
using Prism.Ioc;
using Prism.Modularity;

namespace ioList.Module.Import
{
    public class ImportModule : IModule
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterDialog<DialogShellView>(DialogNames.ImportDialog);
            containerRegistry.RegisterForNavigation<FileSelectionView, FileSelectionViewModel>();
            containerRegistry.RegisterForNavigation<ModuleSelectionView, ModuleSelectionViewModel>();
            containerRegistry.RegisterForNavigation<TagSelectionView, TagSelectionViewModel>();
            containerRegistry.RegisterForNavigation<ImportView, ImportViewModel>();
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
        }
    }
}