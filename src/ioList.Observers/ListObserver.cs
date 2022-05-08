using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using EntityObserver;
using ioList.Domain;

namespace ioList.Observers
{
    public class ListObserver : Observer<List>
    {
        public ListObserver(List model) : base(model)
        {
        }

        [Required(ErrorMessage = "Name is required")]
        [RegularExpression(@"^[\w\-. ]+$",
            ErrorMessage = "Invalid file name characters. Name must alpha numeric or [-. ] characters.")]
        public string Name
        {
            get => GetValue<string>();
            set => SetValue(value);
        }

        [Required(ErrorMessage = "Directory is required")]
        public string Directory
        {
            get => GetValue<string>();
            set => SetValue(value);
        }

        public string Comment
        {
            get => GetValue<string>();
            set => SetValue(value);
        }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!System.IO.Directory.Exists(Directory))
                yield return new ValidationResult($"{Directory} does not exist.", new[] { nameof(Directory) });
            
            if (File.Exists(Entity.FullPath))
                yield return new ValidationResult($"{Entity.FullPath} already exists.", new[] { nameof(Name) });
        }
    }
}