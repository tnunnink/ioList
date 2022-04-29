using System;

namespace ioList.Domain
{
    public class Label
    {
        private Label()
        {
        }
        
        public Label(string label, string description)
        {
            if (string.IsNullOrEmpty(label))
                throw new ArgumentException("Label name can not be null or empty");
            
            Name = label;
            Description = description;

        }
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
    }
}