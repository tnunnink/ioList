using System.Collections.Generic;

namespace ioList.Domain
{
    public class ModuleInfo
    {
        public string CatalogNumber { get; set; }
        public string Description { get; set; }
        public string Vendor { get; set; }
        public string ProductType { get; set; }
        public int ProductCode { get; set; }
        public IEnumerable<string> Revisions { get; set; }
        public IEnumerable<string> Categories { get; set; }
        public IEnumerable<string> PortTypes { get; set; }
    }
}