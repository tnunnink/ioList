using System.Collections.Generic;
using Prism.Mvvm;

namespace ioList.Import
{
    public class ImportContext : BindableBase
    {
        private string _fileName;

        public string FileName
        {
            get => _fileName;
            set => SetProperty(ref _fileName, value);
        }

        private List<string> _moduleNames;

        public List<string> ModuleNames
        {
            get => _moduleNames;
            set => SetProperty(ref _moduleNames, value);
        }
    }
}