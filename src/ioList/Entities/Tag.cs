using System;
using System.Collections.Generic;

namespace ioList.Entities
{
    public class Tag
    {
        private readonly HashSet<TagLabel> _labels = new();
        private readonly List<Comment> _comments = new();

        public Tag()
        {
        }

        public int Id { get; set; }
        public int ModuleId { get; set; }
        public Device Device { get; set; }
        public string Address { get; set; }
        public string DataType { get; set; }
        public string Description { get; set; }
        public string Units { get; set; }
        public string Buffer { get; set; }
        public Scaling Scaling { get; set; }
        public bool Validated { get; set; }
        public string ValidatedBy { get; set; }
        public DateTime ValidatedOn { get; set; }
        public IEnumerable<Reference> References { get; set; }
        public IEnumerable<TagLabel> Labels => _labels;
        public IEnumerable<Comment> Comments => _comments;

        public void Validate()
        {
            Validated = true;
            ValidatedBy = Environment.UserName;
            ValidatedOn = DateTime.Now;
        }

        public void AddLabel(Label label)
        {
            if (label is null)
                throw new ArgumentNullException(nameof(label));

            _labels.Add(new TagLabel(this, label));
        }

        public void RemoveLabel(Label label)
        {
            if (label is null)
                throw new ArgumentNullException(nameof(label));
            
            _labels.RemoveWhere(c => c.LabelId == label.Id);
        }

        public void AddComment(string message)
        {
            if (string.IsNullOrEmpty(message))
                throw new ArgumentException("Message can not be null or empty.");
            
            var comment = new Comment(this, message);
            
            _comments.Add(comment);
        }
    }
}