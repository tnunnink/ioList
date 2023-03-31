using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using ioList.Model;

namespace ioList.ViewModels;

public partial class ConfigColumnViewModel : ObservableValidator
{
    private readonly List<string> _names;

    public ConfigColumnViewModel(TagColumn column, List<string> names = null)
    {
        _names = names;
        ColumnName = column.ColumnName;
    }

    [ObservableProperty] private string _buttonText;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Column name is required")]
    [CustomValidation(typeof(ConfigColumnViewModel), nameof(ValidateName))]
    private string _columnName;

    public void Validate() => ValidateAllProperties();

    public static ValidationResult ValidateName(string name, ValidationContext context)
    {
        var instance = (ConfigColumnViewModel)context.ObjectInstance;

        if (instance._names is null)
            return ValidationResult.Success;

        return instance._names.All(n => n != name)
            ? ValidationResult.Success
            : new ValidationResult($"Column name {name} is already configured. Name must be unique.");
    }
}