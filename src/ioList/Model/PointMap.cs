using CsvHelper.Configuration;

namespace ioList.Model
{
    // ReSharper disable once InconsistentNaming
    public sealed class PointMap : ClassMap<Point>
    {
        public PointMap()
        {
            Map(m => m.ModuleName).Index(0).Name("Module");
            Map(m => m.CatalogNumber).Index(1).Name("Catalog");
            Map(m => m.Tag).Index(2).Name("Tag");
            Map(m => m.ParentName).Index(3).Name("Rack");
            Map(m => m.Slot).Index(4).Name("Slot");
            Map(m => m.Type).Index(5).Name("Type");
            Map(m => m.Path).Index(6).Name("Address");
            Map(m => m.Comment).Index(7).Name("Comment");
            Map(m => m.Unit).Index(8).Name("Units");
            Map(m => m.High).Index(9).Name("High");
            Map(m => m.Low).Index(10).Name("Low");
            Map(m => m.Buffer.TagName).Index(11).Name("Buffer Tag");
            Map(m => m.Buffer.Description).Index(12).Name("Buffer Description");
            Map(m => m.Initials).Index(13).Name("Initials");
            Map(m => m.Date).Index(14).Name("Date");
            Map(m => m.Notes).Index(15).Name("Notes");
        }
    }
}