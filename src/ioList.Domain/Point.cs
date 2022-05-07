using System;
using System.Collections.Generic;
using L5Sharp;
using L5Sharp.Core;

namespace ioList.Domain
{
    public class Point
    {
        private readonly HashSet<PointLabel> _labels;
        private readonly List<Comment> _comments;

        private Point()
        {
        }

        public Point(ITagMember<IDataType> tag, Card card)
        {
            Include = true;
            Address = tag.TagName;
            CardId = card.Id;
            Card = card;
            DataType = tag.DataType.Name;
            Description = tag.Description;
            Units = tag.Units;
            _labels = new HashSet<PointLabel>();
            _comments = new List<Comment>();
        }

        public int Id { get; private set; }
        public bool Include { get; private set; }
        public string Address { get; private set; }
        public string Buffer { get; private set; }
        public int CardId { get; private set; }
        public Card Card { get; private set; }
        public string DataType { get; private set; }
        public string Description { get; private set; }
        public string Units { get; private set; }
        public Scaling Scaling { get; private set; }
        public bool Validated { get; private set; }
        public string ValidatedBy { get; private set; }
        public DateTime ValidatedOn { get; private set; }
        public IEnumerable<PointLabel> Labels => _labels;
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

            _labels.Add(new PointLabel(this, label));
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

        public void SetBuffer(TagName buffer)
        {
            Buffer = buffer ?? throw new ArgumentNullException(nameof(buffer));
        }
        
        public void SetBuffer(Scaling scaling)
        {
            Scaling = scaling ?? throw new ArgumentNullException(nameof(scaling));
        }
    }
}