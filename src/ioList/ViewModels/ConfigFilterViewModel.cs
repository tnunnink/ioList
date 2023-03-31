using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using ioList.Model;

namespace ioList.ViewModels;

public partial class ConfigFilterViewModel : ObservableValidator
{
    public ConfigFilterViewModel(TagFilter filter)
    {
        PropertyNames = new ObservableCollection<string>(typeof(DeviceTag).GetProperties()
            .Where(p => p.PropertyType == typeof(string)).Select(p => p.Name));
        Conditions = new ObservableCollection<FilterCondition>(Enum.GetValues<FilterCondition>());
        PropertyName = filter.PropertyName;
        Condition = filter.Condition;
        Value = filter.Value;
    }

    [ObservableProperty] private string _buttonText;

    [ObservableProperty] [NotifyDataErrorInfo] [Required(ErrorMessage = "Property is required")]
    private string _propertyName;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ValueVisible))]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Condition is required")]
    private FilterCondition _condition;

    [ObservableProperty] [NotifyDataErrorInfo] [CustomValidation(typeof(ConfigFilterViewModel), nameof(ValidateValue))]
    private string _value;

    public Visibility ValueVisible => _condition != FilterCondition.IsEmpty && _condition != FilterCondition.IsNotEmpty
        ? Visibility.Visible
        : Visibility.Collapsed;

    [ObservableProperty] private ObservableCollection<string> _propertyNames;

    [ObservableProperty] private ObservableCollection<FilterCondition> _conditions;

    public void Validate() => ValidateAllProperties();

    public static ValidationResult ValidateValue(string value, ValidationContext context)
    {
        var instance = (ConfigFilterViewModel)context.ObjectInstance;
        var isValid = (instance.ValueVisible == Visibility.Visible && !string.IsNullOrEmpty(instance.Value)) ||
                      instance.ValueVisible == Visibility.Collapsed;

        return isValid
            ? ValidationResult.Success
            : new ValidationResult("Value is required for the selected condition option.");
    }
}