using Infrastructure;
using Infrastructure.Repositories;
using Reactivities.Domain.Interfaces;
using Reactivities.Domain.Models;

namespace Reactivities.Persistence.Repositories
{
    public class PhotoRepository : GenericRepository<Photo>, IPhotoRepository
    {
        public PhotoRepository(DataContext context) : base(context)
        {
        }
    }
}
