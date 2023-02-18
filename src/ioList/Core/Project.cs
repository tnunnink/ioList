using System;
using System.Data;
using System.IO;
using System.IO.Abstractions;
using System.Threading;
using System.Threading.Tasks;
using FluentMigrator.Runner;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.DependencyInjection;
using NLog.Extensions.Logging;

namespace ioList.Core
{
    public class Project
    {
        private const string DefaultName = "MyProject";
        private static readonly string DefaultLocation =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), @"ioList\Projects");

        private readonly IFileSystem _fileSystem;
        private readonly IFileInfo _fileInfo;
        private readonly IDirectoryInfo _directoryInfo;

        public Project(IFileSystem fileSystem = null)
        {
            _fileSystem = fileSystem ?? new FileSystem();
            _directoryInfo = InitializeDirectory(DefaultLocation);
            _fileInfo = _fileSystem.FileInfo.New(_fileSystem.Path.Combine(DefaultLocation, $"{DefaultName}.db"));
        }

        public Project(string fileName, IFileSystem fileSystem = null)
        {
            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentException("fileName can not be null or empty.");

            _fileSystem = fileSystem ?? new FileSystem();
            _directoryInfo = InitializeDirectory(_fileSystem.Path.GetDirectoryName(fileName));
            _fileInfo = _fileSystem.FileInfo.New(fileName);
        }

        /// <summary>
        /// Creates a new project entity with the specified name and default project location.
        /// </summary>
        /// <param name="name">The name of the project. This represents the file name.
        /// Will add .db extension if not provided. Will default to MyProject if null or empty.</param>
        /// <param name="location"></param>
        /// <param name="fileSystem"></param>
        public Project(string name, string location, IFileSystem fileSystem = null)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("name can not be null or empty.");

            if (string.IsNullOrEmpty(location))
                throw new ArgumentException("location can not be null or empty.");

            _fileSystem = fileSystem ?? new FileSystem();
            _directoryInfo = InitializeDirectory(DefaultLocation);
            _fileInfo = _fileSystem.FileInfo.New(_fileSystem.Path.Combine(location, $"{name}.db"));
        }

        /// <summary>
        /// Gets the name of the current project file.
        /// </summary>
        public string Name => _fileSystem.Path.GetFileNameWithoutExtension(_fileInfo.Name);

        /// <summary>
        /// Gets the directory location of the current project file.
        /// </summary>
        public string Location => _directoryInfo.FullName;

        /// <summary>
        /// Gets the full path of the project file including the .db extension.
        /// </summary>
        public string FileName => _fileInfo.FullName;

        /// <summary>
        /// Gets a value indicating whether the file or directory exists.
        /// </summary>
        public bool Exists => _fileInfo.Exists;

        /// <summary>
        /// Gets or sets the time when the current file or directory was last written to.
        /// </summary>
        public DateTime AccessedOn => _fileInfo.LastAccessTime;

        /// <summary>
        /// Gets or sets the time when the current file or directory was last written to.
        /// </summary>
        public DateTime CreatedOn => _fileInfo.CreationTime;

        /// <summary>
        /// Gets or sets the time when the current file or directory was last written to.
        /// </summary>
        public DateTime UpdatedOn => _fileInfo.LastWriteTime;

        /// <summary>
        /// Gets the Sqlite database connection string for the project file.
        /// </summary>
        public string ConnectionString => GetConnectionString();

        /// <summary>
        /// Gets the connection to the project file database.
        /// </summary>
        /// <returns>A <see cref="IDbConnection"/> instance</returns>
        public IDbConnection Connect() => new SqliteConnection(GetConnectionString());
        
        /// <summary>
        /// Gets a new <see cref="IFileSystemWatcher"/> to monitoring the project file.
        /// </summary>
        /// <returns>A <see cref="IFileSystemWatcher"/> instance.</returns>
        public IFileSystemWatcher Watcher()
        {
            var watcher = _fileSystem.FileSystemWatcher.New(Location);

            watcher.Filter = _fileInfo.Name;
            watcher.EnableRaisingEvents = true;

            return watcher;
        }

        /// <summary>
        /// Creates the current project file assuming is does not already exist.
        /// </summary>
        public void Create()
        {
            //Opening a connection will create the file.
            using var connection = new SqliteConnection(GetConnectionString());
            connection.Open();
        }

        /// <summary>
        /// Runs any required migrations for the current project to bring the database file up to latest version.
        /// </summary>
        public void Migrate()
        {
            //this is to prevent fluent migrator from holder a database lock
            var migrationConnection =
                new SqliteConnectionStringBuilder(GetConnectionString()) { Pooling = false }.ConnectionString;

            using var serviceProvider = CreateServices(migrationConnection);
            using var scope = serviceProvider.CreateScope();
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

            runner.MigrateUp();
        }

        /// <summary>
        /// Renames the current project file.
        /// </summary>
        /// <param name="name">The new name of the project.</param>
        public void Rename(string name)
        {
            var fileName = _fileSystem.Path.Combine(Location, $"{name}.db");
            _fileInfo.MoveTo(fileName, true);
        }

        /// <summary>
        /// Moves the current project file to the specified location on disc.
        /// </summary>
        /// <param name="location">The file directory in which to move the project.</param>
        public void Move(string location)
        {
            var fileName = _fileSystem.Path.Combine(location, Name);
            _fileInfo.MoveTo(fileName, true);
        }

        /// <summary>
        /// Deletes the project file from disc.
        /// </summary>
        public void Delete() => _fileInfo.Delete();

        private ServiceProvider CreateServices(string connectionString)
        {
            return new ServiceCollection()
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddSQLite()
                    .WithGlobalConnectionString(connectionString)
                    .ScanIn(GetType().Assembly).For.Migrations())
                .AddLogging(lb => lb.AddNLog())
                .BuildServiceProvider(false);
        }

        private string GetConnectionString() =>
            new SqliteConnectionStringBuilder { DataSource = FileName }.ConnectionString;

        private IDirectoryInfo InitializeDirectory(string location)
        {
            var info = _fileSystem.DirectoryInfo.New(location);
            if (!info.Exists) info.Create();
            return info;
        }
    }
}