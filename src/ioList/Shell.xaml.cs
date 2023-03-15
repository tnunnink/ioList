namespace ioList.Views
{
    public partial class Shell
    {
        public Shell()
        {
            InitializeComponent();
            DataContext = new ViewModels.ShellViewModel();
        }
    }
}