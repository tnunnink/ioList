namespace ioList.Entities
{
    public class ImportSettings
    {
        public ConflictSetting ConflictSetting { get; set; }
        public bool AutoDetectTags { get; set; }
        public bool AutoDetectBuffers { get; set; }
        public bool AutoDetectScaling { get; set; }
        /*public IEnumerable<Func<Module, bool>> ModuleFilters { get; set; }
        public IEnumerable<Func<Tag, bool>> TagFilters { get; set; }
        public IEnumerable<string> IgnoreMembers { get; set; }
        public IEnumerable<string> BufferPatterns { get; set; }
        public IEnumerable<string> ScalingMembers { get; set; }*/

        public static ImportSettings Default()
        {
            return new ImportSettings
            {
                ConflictSetting = ConflictSetting.Replace,
                AutoDetectTags = true,
                AutoDetectBuffers = true,
                AutoDetectScaling = true
            };
        } 
    }
}