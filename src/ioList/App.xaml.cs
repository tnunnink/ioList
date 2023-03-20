using System;
using System.Diagnostics;
using System.Windows;
using ioList.Model;
using ioList.ViewModels;
using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.DependencyInjection;
using Squirrel;

namespace ioList
{
    public partial class App
    {
        public App()
        {
            Services = ConfigureServices();
            InitializeComponent();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            using (var eventLog = new EventLog("Application"))
            {
                eventLog.Source = "Application";
                eventLog.WriteEntry("ioList Entered Startup.", EventLogEntryType.Information);
            }

            SquirrelAwareApp.HandleEvents(
                onInitialInstall: OnAppInstall,
                onAppUninstall: OnAppUninstall,
                onEveryRun: OnAppRun);
        }
        
        /// <summary>
        /// Gets the current <see cref="App"/> instance in use
        /// </summary>
        public new static App Current => (App)Application.Current;

        /// <summary>
        /// Gets the <see cref="IServiceProvider"/> instance to resolve application services.
        /// </summary>
        public IServiceProvider Services { get; }
        
        public const string RepositoryUrl = @"https://github.com/tnunnink/ioList";

        public const string ReadMeUrl = @"https://github.com/tnunnink/ioList/blob/main/README.md";

        public const string IssuesUrl = @"https://github.com/tnunnink/ioList/issues";

        /// <summary>
        /// Configures the services for the application.
        /// </summary>
        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddSingleton(GeneratorOptions.Load());
            services.AddSingleton<ISnackbarMessageQueue, SnackbarMessageQueue>();
            services.AddTransient<ShellViewModel>();
            services.AddTransient<FooterViewModel>();
            services.AddTransient<OptionsViewModel>();

            return services.BuildServiceProvider();
        }

        private static void OnAppInstall(SemanticVersion version, IAppTools tools)
        {
            tools.CreateShortcutForThisExe(ShortcutLocation.StartMenu);
        }

        private static void OnAppUninstall(SemanticVersion version, IAppTools tools)
        {
            tools.RemoveShortcutForThisExe(ShortcutLocation.StartMenu);
        }

        private static void OnAppRun(SemanticVersion version, IAppTools tools, bool firstRun)
        {
            tools.SetProcessAppUserModelId();
            // show a welcome message when the app is first installed
            if (firstRun) MessageBox.Show("Thanks for installing my application!");
        }
    }
}