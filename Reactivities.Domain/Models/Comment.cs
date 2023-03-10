using Domain.Models;

namespace Reactivities.Domain.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public virtual User Author { get; set; }
        public virtual Activity Activity { get; set; }
        public DateTimeOffset CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
