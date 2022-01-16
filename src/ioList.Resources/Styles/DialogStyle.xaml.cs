using System.Windows;

namespace ioList.Resources.Styles
{
    public partial class DialogStyle
    {
        public DialogStyle()
        {
            InitializeComponent();
        }

        private void OnCloseClick(object sender, RoutedEventArgs e)
        {
            var window = (Window)((FrameworkElement)sender).TemplatedParent;
            window.Close();
        }
    }
}