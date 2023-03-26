using Reactivities.Application.Core;

namespace Reactivities.Application.Models.Activities
{
    public class ActivityParams : PagingParams
    {
        public bool IsGoing { get; set; }
        public bool IsHost { get; set; }
        public DateTimeOffset StartDate { get; set; } = DateTime.UtcNow;
    }
}
