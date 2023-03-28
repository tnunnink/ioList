using System.Collections.Generic;
using ioList.Entities;
using L5Sharp.Enums;

namespace ioList.Generation;

public class GeneratorConfig
{
    public bool Overwrite { get; set; } = true;
    
    public bool FilterUnused { get; set; } = true;
    public bool FindBuffers { get; set; } = true;

    public List<BufferPattern> Patterns { get; set; } = new()
    {
        new BufferPattern(Instruction.MOV, 1),
        new BufferPattern(Instruction.CPT),
        new BufferPattern(Instruction.XIC, Instruction.OTE),
        new BufferPattern(Instruction.XIO, Instruction.OTE),
        new BufferPattern(Instruction.OTE, Instruction.XIC),
        new BufferPattern(Instruction.OTE, Instruction.XIO),
    };

    public List<TagFilter> Filters { get; set; } = new();
}