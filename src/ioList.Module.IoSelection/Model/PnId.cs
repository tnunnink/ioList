using System.Text.RegularExpressions;

namespace ioList.Module.IoSelection.Model
{
    public class PnId
    {
        public PnId(string tagName)
        {
            if (string.IsNullOrEmpty(tagName)) return;

            TagName = tagName;

            var parts = tagName.Split('_');

            if (parts.Length != 3) return;
            
            Prefix = parts[0];
            Area = Regex.Match(parts[1], @"[0-9]+").Value;
            Type = Regex.Match(parts[1], @"[A-Z]+").Value;
            DeviceId = parts[2];
        }

        public string TagName { get; }
        public string Prefix { get; }
        public string Area { get; }
        public string Type { get; }
        public string DeviceId { get; }
    }
}