using Domain.Models;
using Infrastructure;
using Infrastructure.Repositories;
using Reactivities.Domain.Interfaces;

namespace Reactivities.Persistence.Repositories
{
    public class ActivityAttendeeRepository : GenericRepository<ActivityAttendee>, IActivityAttendeeRepository
    {
        public ActivityAttendeeRepository(DataContext context) : base(context)
        {
        }
    }
}
