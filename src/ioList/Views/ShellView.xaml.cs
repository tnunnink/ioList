using System.Windows;
using ioList.Common;
using ioList.Module.LoadFile.Views;
using Prism.Regions;

namespace ioList.Views
{
    public partial class ShellView : Window
    {
        public ShellView(IRegionManager regionManager)
        {
            InitializeComponent();

            regionManager.RegisterViewWithRegion<MainView>(RegionNames.ContentRegion);
        }
    }
}