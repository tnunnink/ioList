using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using EntityObserver;
using ioList.Domain;
using L5Sharp;

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

        public string Comment
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

        [Required(ErrorMessage = "Source File is required")]
        public string SourceFile
        {
            get => GetValue<string>();
            set => SetValue(value);
        }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!CanLoadFile())
                yield return new ValidationResult($"{SourceFile} is not a valid L5X file.",
                    new[] { nameof(SourceFile) });

            if (!System.IO.Directory.Exists(Directory))
                yield return new ValidationResult($"{Directory} is not a valid existing path.",
                    new[] { nameof(Directory) });

            var fullName = Path.Combine(Directory, $"{Name}.lst");
            if (File.Exists(fullName))
                yield return new ValidationResult($"{fullName} already exists.", new[] { nameof(Name) });
        }

        private bool CanLoadFile()
        {
            try
            {
                LogixContext.Load(SourceFile);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}