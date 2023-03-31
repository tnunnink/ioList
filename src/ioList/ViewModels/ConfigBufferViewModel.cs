using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using ioList.Model;

namespace ioList.ViewModels;

public partial class ConfigBufferViewModel : ObservableValidator
{
    public ConfigBufferViewModel(BufferPattern pattern)
    {
        ReferenceKey = pattern.ReferenceKey;
        BufferKey = pattern.BufferKey;
        BufferOrdinal = pattern.BufferOrdinal;
    }

    [ObservableProperty] private string _buttonText;

    [ObservableProperty] 
    [NotifyDataErrorInfo] 
    [Required(ErrorMessage = "Reference is required")]
    private string _referenceKey;
    
    [ObservableProperty] 
    [NotifyDataErrorInfo] 
    [Required(ErrorMessage = "Buffer is required")]
    private string _bufferKey;
    
    [ObservableProperty] 
    [NotifyDataErrorInfo] 
    [Required(ErrorMessage = "Ordinal is required")]
    private int _bufferOrdinal;

    public void Validate() => ValidateAllProperties();
}