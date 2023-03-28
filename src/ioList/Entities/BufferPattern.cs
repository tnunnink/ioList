using L5Sharp.Enums;

namespace ioList.Entities;

public class BufferPattern
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

    public string ReferenceKey { get; set; }
    public string BufferKey { get; set; }
    public int BufferOrdinal { get; set; }
}