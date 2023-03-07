using Reactivities.Domain.Interfaces;
using Reactivities.Domain.Models;

namespace Reactivities.Application.Queries.Photos
{
    public interface IPhotosQueryBuilder
    {
        IQueryable<Photo> GetPhotoEntity(IPhotoRepository photoRepository);
    }
}