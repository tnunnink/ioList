using Microsoft.Win32;

namespace ioList.Module.LoadFile.Services
{
    public class LoadFileService : ILoadFileService
    {
        public string OpenFile()
        {
            var dialog = new OpenFileDialog
            {
                DefaultExt = ".L5X",
                Filter = "XML Files(.L5X, .xml)|*.L5X;*.xml",
                Multiselect = false
            };

            var result = dialog.ShowDialog();

            return result == true ? dialog.FileName : string.Empty;
        }
    }
}