using CommunityToolkit.Mvvm.ComponentModel;
using L5Sharp.Enums;

namespace ioList.Model;

public partial class BufferPattern : ObservableObject
{
    public BufferPattern()
    {
    }

    public BufferPattern(string referenceKey, string bufferKey, int bufferOrdinal = 0)
    {
        ReferenceKey = referenceKey;
        BufferKey = bufferKey;
        BufferOrdinal = bufferOrdinal;
    }
    
    public BufferPattern(Instruction reference, Instruction buffer, int bufferOrdinal = 0)
    {
        ReferenceKey = reference.Name;
        BufferKey = buffer.Name;
        BufferOrdinal = bufferOrdinal;
    }

    public BufferPattern(Instruction buffer, int bufferOrdinal = 0)
    {
        ReferenceKey = buffer.Name;
        BufferKey = buffer.Name;
        BufferOrdinal = bufferOrdinal;
    }

    [ObservableProperty]
    private bool _enabled = true;
    
    [ObservableProperty]
    private string _referenceKey;
    
    [ObservableProperty]
    private string _bufferKey;
    
    [ObservableProperty]
    private int _bufferOrdinal;
}