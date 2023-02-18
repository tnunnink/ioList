using System;
using System.IO;
using CoreWPF.Services;

namespace ioList.Shared
{
    public static class Global
    {
        public static readonly string Directory =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"ioList");

        public const string RepositoryUrl = @"https://github.com/tnunnink/ioList";

        public const string IssuesUrl = @"https://github.com/tnunnink/ioList/issues";

        public const string PagesUrl = @"https://github.com/tnunnink/ioList/issues";

        public static PersistenceStore Settings => PersistenceStore.Default(@"ioList\settings");
        
        public static PersistenceStore Data => PersistenceStore.Default(@"ioList\data");
    }
}