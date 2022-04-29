using System;

namespace ioList.Domain
{
    public class Comment
    {
        private Comment()
        {
        }
        
        public Comment(Point point, string message)
        {
            Id = 0;
            PointId = point.Id;
            Point = point;
            User = Environment.UserName;
            Message = message;
            AddedOn = DateTime.Now;
        }
        
        public int Id { get; private set; }
        public int PointId { get; private set; }
        public Point Point { get; private set; }
        public string User { get; private set; }
        public string Message { get; private set; }
        public DateTime AddedOn { get; private set; }
    }
}