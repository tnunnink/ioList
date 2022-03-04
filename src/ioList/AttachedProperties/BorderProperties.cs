using System.Windows;

// ReSharper disable UnusedMember.Global These are used by XAML parser

namespace ioList.Resources.Properties
{
    public static class BorderProperties
    {
        /// <summary>
        /// Corner Radius Property.
        /// Adds the ability to set a corner radius on the button control.  
        /// </summary>
        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.RegisterAttached(
            "CornerRadius", typeof(CornerRadius), typeof(BorderProperties),
            new PropertyMetadata(default(CornerRadius)));

        public static void SetCornerRadius(DependencyObject element, CornerRadius value) =>
            element.SetValue(CornerRadiusProperty, value);

        
        public static CornerRadius GetCornerRadius(DependencyObject element) =>
            (CornerRadius)element.GetValue(CornerRadiusProperty);
    }
}