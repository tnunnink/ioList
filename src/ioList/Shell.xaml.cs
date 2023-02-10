using System.Windows;

namespace ioList
{
    public partial class Shell
    {
        public Shell()
        {
            InitializeComponent();
            
            Loaded += OnLoaded;
        }
        
        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            SizeToContent = SizeToContent.WidthAndHeight;
            Loaded -= OnLoaded;
        }
    }
}