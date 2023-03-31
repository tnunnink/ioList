using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ioList.Model;
using ioList.Views;
using MaterialDesignThemes.Wpf;

namespace ioList.ViewModels;

public partial class ConfigurationViewModel : ObservableObject
{
    public ConfigurationViewModel(GeneratorConfig config)
    {
        _config = config;
    }

    [ObservableProperty] private GeneratorConfig _config;

    [RelayCommand]
    private async Task AddFilter()
    {
        var viewModel = new ConfigFilterViewModel(new TagFilter())
        {
            ButtonText = "Add"
        };
        var view = new ConfigFilterView { DataContext = viewModel };

        var result = await DialogHost.Show(view, "ConfigHost", ConfigFilterClosing);

        if (result is not ConfigFilterViewModel vm) return;

        var filter = new TagFilter(vm.PropertyName, vm.Condition, vm.Value);
        _config.Filters.Add(filter);
    }

    [RelayCommand]
    private static async Task EditFilter(TagFilter filter)
    {
        var viewModel = new ConfigFilterViewModel(filter)
        {
            ButtonText = "Update"
        };
        var view = new ConfigFilterView { DataContext = viewModel };

        var result = await DialogHost.Show(view, "ConfigHost", ConfigFilterClosing);

        if (result is not ConfigFilterViewModel vm) return;

        filter.PropertyName = vm.PropertyName;
        filter.Condition = vm.Condition;
        filter.Value = vm.Value;
    }

    private static void ConfigFilterClosing(object sender, DialogClosingEventArgs args)
    {
        if (args.Parameter is not ConfigFilterViewModel vm) return;

        vm.Validate();

        if (!vm.HasErrors) return;

        args.Cancel();
    }

    [RelayCommand(CanExecute = nameof(CanRemoveFilter))]
    private void RemoveFilter(TagFilter filter) => _config.Filters.Remove(filter);

    private static bool CanRemoveFilter(TagFilter filter) => filter is not null;

    [RelayCommand]
    private async Task AddBuffer()
    {
        var viewModel = new ConfigBufferViewModel(new BufferPattern())
        {
            ButtonText = "Add"
        };

        var view = new ConfigBufferView { DataContext = viewModel };

        var result = await DialogHost.Show(view, "ConfigHost", ConfigBufferClosing);

        if (result is not ConfigBufferViewModel vm) return;

        var pattern = new BufferPattern(vm.ReferenceKey, vm.BufferKey, vm.BufferOrdinal);
        _config.Patterns.Add(pattern);
    }

    [RelayCommand]
    private static async Task EditBuffer(BufferPattern pattern)
    {
        var viewModel = new ConfigBufferViewModel(pattern)
        {
            ButtonText = "Update"
        };
        var view = new ConfigBufferView { DataContext = viewModel };

        var result = await DialogHost.Show(view, "ConfigHost", ConfigBufferClosing);

        if (result is not ConfigBufferViewModel vm) return;

        pattern.ReferenceKey = vm.ReferenceKey;
        pattern.BufferKey = vm.BufferKey;
        pattern.BufferOrdinal = vm.BufferOrdinal;
    }

    private static void ConfigBufferClosing(object sender, DialogClosingEventArgs args)
    {
        if (args.Parameter is not ConfigBufferViewModel vm) return;

        vm.Validate();

        if (!vm.HasErrors) return;

        args.Cancel();
    }

    [RelayCommand(CanExecute = nameof(CanRemoveBuffer))]
    private void RemoveBuffer(BufferPattern pattern) => _config.Patterns.Remove(pattern);

    private static bool CanRemoveBuffer(BufferPattern pattern) => pattern is not null;

    [RelayCommand]
    private async Task AddColumn()
    {
        var viewModel = new ConfigColumnViewModel(new TagColumn(), _config.Columns.Select(c => c.ColumnName).ToList())
        {
            ButtonText = "Add"
        };
        var view = new ConfigColumnView { DataContext = viewModel };

        var result = await DialogHost.Show(view, "ConfigHost", ConfigColumnClosing);

        if (result is not ConfigColumnViewModel vm) return;

        var column = new TagColumn(ColumnType.Field, vm.ColumnName, _config.Columns.Count);
        _config.Columns.Add(column);
    }

    [RelayCommand]
    private static async Task EditColumn(TagColumn column)
    {
        var viewModel = new ConfigColumnViewModel(column)
        {
            ButtonText = "Update"
        };
        var view = new ConfigColumnView { DataContext = viewModel };

        var result = await DialogHost.Show(view, "ConfigHost", ConfigColumnClosing);

        if (result is not ConfigColumnViewModel vm) return;

        column.ColumnName = vm.ColumnName;
    }

    private static void ConfigColumnClosing(object sender, DialogClosingEventArgs args)
    {
        if (args.Parameter is not ConfigColumnViewModel vm) return;

        vm.Validate();

        if (!vm.HasErrors) return;

        args.Cancel();
    }

    [RelayCommand(CanExecute = nameof(CanRemoveColumn))]
    private void RemoveColumn(TagColumn column) => _config.Columns.Remove(column);

    private static bool CanRemoveColumn(TagColumn column) => column is not null && column.Type != ColumnType.Property;
}