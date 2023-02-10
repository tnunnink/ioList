using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using EntityObserver;
using ioList.Entities;

namespace ioList.Observers
{
    public class ProjectFileObserver : Observer<ProjectFile>
    {
        private readonly FileInfo _fileInfo;

        public ProjectFileObserver() : base(new ProjectFile("MyProject"))
        {
            _fileInfo = new FileInfo(Entity.FileName());
        }

        public ProjectFileObserver(ProjectFile entity) : base(entity)
        {
            _fileInfo = new FileInfo(Entity.FileName());
        }

        [Required(ErrorMessage = "Name is required")]
        [RegularExpression(@"^[\w\-. ]+$",
            ErrorMessage = "Invalid file name characters. Name must contain alpha numeric or [-. ] characters.")]
        public string Name
        {
            get => GetValue<string>();
            set => SetValue(value);
        }

        [Required(ErrorMessage = "Location is required")]
        public string Location
        {
            get => GetValue<string>();
            set => SetValue(value);
        }
        
        public bool Exists => _fileInfo.Exists;

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!Directory.Exists(Location))
                yield return new ValidationResult($"{Location} does not exist.", new[] { nameof(Location) });
            
            if (File.Exists(Entity.FileName()))
                yield return new ValidationResult($"{Entity.FileName()} already exists.", new[] { nameof(Name) });
        }
    }
}