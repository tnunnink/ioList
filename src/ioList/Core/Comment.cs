using System;

namespace ioList.Core
{
    public class Comment
    {
        private Comment()
        {
        }
        
        public Comment(Tag tag, string message)
        {
            Id = 0;
            TagId = tag.Id;
            Tag = tag;
            User = Environment.UserName;
            Message = message;
            AddedOn = DateTime.Now;
        }
        
        public int Id { get; private set; }
        public int TagId { get; private set; }
        public Tag Tag { get; private set; }
        public string User { get; private set; }
        public string Message { get; private set; }
        public DateTime AddedOn { get; private set; }
    }
}