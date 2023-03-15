using ioList.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace ioList.Views
{
    public partial class FooterView
    {
        public FooterView()
        {
            InitializeComponent();
            DataContext = App.Current.Services.GetService<FooterViewModel>();
        }
    }
}