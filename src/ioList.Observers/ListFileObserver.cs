using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using EntityObserver;
using ioList.Entities;

namespace ioList.Observers
{
    public class ListFileObserver : Observer<ListFile>
    {
        private bool _building;

        public ListFileObserver() : base(new ListFile(@"C:\Users\tnunnink\Desktop\My Lists\List Name.xml"))
        {
        }

        public ListFileObserver(ListFile model) : base(model)
        {
        }

        [Required(ErrorMessage = "Name is required")]
        [RegularExpression(@"^[\w\-. ]+$",
            ErrorMessage = "Invalid file name characters. Name must alpha numeric or [-. ] characters.")]
        public string Name
        {
            get => GetValue<string>();
            set => SetValue(value, setter: (f, s) => f.Rename(s));
        }

        public string DirectoryName => GetValue<string>();

        public bool Exists => GetValue<bool>();

        public bool Building
        {
            get => _building;
            set
            {
                RaisePropertyChanged();
                _building = value;
            }
        }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (File.Exists(Entity.FullName))
                yield return new ValidationResult($"{Entity.FullName} already exists.", new[] { nameof(Name) });
        }
    }
}