using ioList.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace ioList
{
    public partial class Shell
    {
        public Shell()
        {
            InitializeComponent();
            DataContext = App.Current.Services.GetService<ShellViewModel>();
            Left = System.Windows.SystemParameters.WorkArea.Width/2 - Width/2;
            Top = System.Windows.SystemParameters.WorkArea.Height/2 - Height/2;
        }
    }
}