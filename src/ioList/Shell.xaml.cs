namespace ioList
{
    public partial class Shell
    {
        public Shell()
        {
            InitializeComponent();
            DataContext = new ShellViewModel();
        }
    }
}