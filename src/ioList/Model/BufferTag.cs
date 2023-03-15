using L5Sharp;
using L5Sharp.Core;

namespace ioList.Model;

public class BufferTag
{
    public BufferTag(ILogixTag tag)
    {
        if (tag is null) return;

        TagName = tag.TagName;
        Description = tag.Description;
    }

    public TagName TagName { get; set; } = TagName.Empty;
    public string Description { get; set; } = string.Empty;
}