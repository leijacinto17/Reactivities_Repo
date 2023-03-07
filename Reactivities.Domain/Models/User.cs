using Microsoft.AspNetCore.Identity;
using Reactivities.Domain.Models;

namespace Domain.Models
{
    public class User : IdentityUser
    {
        public string DisplayName { get; set; }
        public string Bio { get; set; }
        public virtual ICollection<ActivityAttendee> Activities { get; set; }
        public virtual ICollection<Photo> Photos { get; set; }
    }
}
