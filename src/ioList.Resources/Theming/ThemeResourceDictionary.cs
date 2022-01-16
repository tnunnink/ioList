using System.Windows;

namespace ioList.Resources.Theming
{
    public sealed class ThemeResourceDictionary : ResourceDictionary
    {
        public ThemeResourceDictionary()
        {
            MergedDictionaries.Add(new ResourceDictionary());
        }
    }
}