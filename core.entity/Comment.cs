using System;

namespace core.entity
{
    public class Comment
    {
        public int CommentId { get; set; }
        public int ProductId { get; set; }
        public string UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double? Rate { get; set; }
        public bool IsApproved { get; set; }
        public DateTime DateAdded { get; set; }
    }
}