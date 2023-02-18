using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using EntityObserver;

namespace ioList.Features.Startup
{
    public class ProjectObserver : Observer<ProjectFile>
    {
        public ProjectObserver() : base(new ProjectFile())
        {
        }

        public ProjectObserver(ProjectFile entity) : base(entity)
        {
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

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!Directory.Exists(Location))
                yield return new ValidationResult($"{Location} does not exist.", new[] { nameof(Location) });
            
            if (File.Exists(Entity.FileName))
                yield return new ValidationResult($"{Entity.FileName} already exists.", new[] { nameof(Name) });
        }
    }
}