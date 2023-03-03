using Domain.Interfaces;
using Domain.Models;

namespace Infrastructure.Repositories
{
    public class ActivitiesRepository : GenericRepository<Activity>, IActivitiesRepository
    {
        public ActivitiesRepository(DataContext context) : base(context)
        {
        }
    }
}
