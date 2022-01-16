using System.Windows;
using System.Windows.Controls;

namespace ioList.Resources.Controls
{
    public class IconButton : Button
    {
        public ControlTemplate DefaultIcon
        {
            get => (ControlTemplate) GetValue(DefaultIconProperty);
            set => SetValue(DefaultIconProperty, value);
        }

        public static readonly DependencyProperty DefaultIconProperty =
            DependencyProperty.Register(nameof(DefaultIcon), typeof(ControlTemplate), typeof(IconButton),
                new PropertyMetadata(default(ControlTemplate)));

        public ControlTemplate MouseOverIcon
        {
            get => (ControlTemplate) GetValue(MouseOverIconProperty);
            set => SetValue(MouseOverIconProperty, value);
        }

        public static readonly DependencyProperty MouseOverIconProperty =
            DependencyProperty.Register(nameof(MouseOverIcon), typeof(ControlTemplate), typeof(IconButton),
                new PropertyMetadata(default(ControlTemplate)));
    }
}