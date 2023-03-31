using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using CommunityToolkit.Mvvm.ComponentModel;
using L5Sharp.Enums;

namespace ioList.Model;

public partial class GeneratorConfig : ObservableObject
{
    private const string FileName = "Config.json";
    
    [ObservableProperty]
    private bool _overwrite = true;

    [ObservableProperty]
    private bool _filterUnused = true;
    
    [ObservableProperty]
    private bool _findBuffers = true;

    [ObservableProperty]
    private ObservableCollection<BufferPattern> _patterns = new()
    {
        new BufferPattern(Instruction.MOV, 1),
        new BufferPattern(Instruction.MOV),
        new BufferPattern(Instruction.CPT),
        new BufferPattern(Instruction.XIC, Instruction.OTE),
        new BufferPattern(Instruction.XIO, Instruction.OTE),
        new BufferPattern(Instruction.OTE, Instruction.XIC),
        new BufferPattern(Instruction.OTE, Instruction.XIO)
    };
    
    [ObservableProperty]
    private ObservableCollection<TagFilter> _filters = new();

    [ObservableProperty] private ObservableCollection<TagColumn> _columns = new()
    {
        new TagColumn(nameof(DeviceTag.Module), 0),
        new TagColumn(nameof(DeviceTag.Catalog), 1),
        new TagColumn(nameof(DeviceTag.TagName), 2),
        new TagColumn(nameof(DeviceTag.Type), 3),
        new TagColumn(nameof(DeviceTag.Rack), 4),
        new TagColumn(nameof(DeviceTag.Slot), 5),
        new TagColumn(nameof(DeviceTag.Address), 6),
        new TagColumn(nameof(DeviceTag.Comment), 7),
        new TagColumn(nameof(DeviceTag.Unit), 8),
        new TagColumn(nameof(DeviceTag.High), 9),
        new TagColumn(nameof(DeviceTag.Low), 10),
        new TagColumn("Buffer Tag", nameof(DeviceTag.BufferTag), 11),
        new TagColumn("Buffer Description", nameof(DeviceTag.BufferDescription), 12),
        new TagColumn(ColumnType.Field, "Initials", 13),
        new TagColumn(ColumnType.Field, "Date", 14),
        new TagColumn(ColumnType.Field, "Notes", 15),
    };

    public static GeneratorConfig Load()
    {
        var file = new FileInfo(FileName);

        if (!file.Exists)
            File.WriteAllText(FileName, JsonSerializer.Serialize(new GeneratorConfig()));

        return JsonSerializer.Deserialize<GeneratorConfig>(File.ReadAllText(FileName));
    }

    public void Save()
    {
        File.WriteAllText(FileName, JsonSerializer.Serialize(this));
    }
}