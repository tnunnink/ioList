using System;

namespace ioList.Domain
{
    public class Comment
    {
        public int Id { get; set; }
        public int PointId { get; set; }
        public string User { get; set; }
        public string Message { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}