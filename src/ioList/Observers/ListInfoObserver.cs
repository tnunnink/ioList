using EntityObserver;
using ioList.Model;

namespace ioList.Observers
{
    public class ListInfoObserver : Observer<ListInfo>
    {
        public ListInfoObserver(ListInfo model) : base(model)
        {
        }

        public string Name
        {
            get => GetValue<string>();
            set => SetValue(value);
        }

        public string Comment
        {
            get => GetValue<string>();
            set => SetValue(value);
        }

        public string Directory
        {
            get => GetValue<string>();
            set => SetValue(value);
        }

        public string SourceFile
        {
            get => GetValue<string>();
            set => SetValue(value);
        }

        public bool IncludeReferences
        {
            get => GetValue<bool>();
            set => SetValue(value);
        }
    }
}