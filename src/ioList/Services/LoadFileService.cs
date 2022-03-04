using System;
using ioList.Abstractions;

namespace ioList.Services
{
    public class LoadFileService : ILoadFileService
    {
        public string OpenFile()
        {
            /*var dialog = new OpenFileDialog
            {
                DefaultExt = ".L5X",
                Filter = "XML Files(.L5X, .xml)|*.L5X;*.xml",
                Multiselect = false
            };

            var result = dialog.ShowDialog();

            return result == true ? dialog.Name : string.Empty;*/
            throw new NotImplementedException();
        }
    }
}