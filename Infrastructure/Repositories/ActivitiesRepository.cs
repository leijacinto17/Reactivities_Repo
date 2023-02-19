using Domain.Interfaces;
using Domain.Models;

namespace Infrastructure.Repositories
{
    public class ActivitiesRepository : GenericRepository<Activity>, IActivitiesRepository
    {
        private readonly DataContext _context;
        public ActivitiesRepository(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
