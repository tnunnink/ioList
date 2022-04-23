using ioList.Common;
using ioList.Module.Settings.ViewModels;
using ioList.Module.Settings.Views;
using Prism.Ioc;
using Prism.Modularity;

namespace ioList.Module.Settings
{
    public class SettingsModule : IModule
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterDialog<SettingsView, SettingsViewModel>(DialogNames.SettingsDialog);
            containerRegistry.RegisterDialog<NewListView, NewListViewModel>(DialogNames.NewListDialog);
            
            containerRegistry.RegisterForNavigation<FileSettingsView, FileSettingsViewModel>();
            containerRegistry.RegisterForNavigation<ModuleSettingsView, ModuleSettingsViewModel>();
            containerRegistry.RegisterForNavigation<FilterSettingsView, FilterSettingsViewModel>();
            containerRegistry.RegisterForNavigation<BufferSettingsView, BufferSettingsViewModel>();
            containerRegistry.RegisterForNavigation<ScalingSettingsView, ScalingSettingsViewModel>();
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            
        }
    }
}