namespace Domain.Models
{
    public class ActivityAttendee
    {
        public string UserId { get; set; }
        public virtual User User { get; set; }
        public Guid ActivityId { get; set; }
        public virtual Activity Activity { get; set; }
        public bool IsHost { get; set; }
    }
}
