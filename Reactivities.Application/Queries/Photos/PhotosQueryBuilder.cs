using Reactivities.Domain.Interfaces;
using Reactivities.Domain.Models;

namespace Reactivities.Application.Queries.Photos
{
    public class PhotosQueryBuilder : IPhotosQueryBuilder
    {
        public IQueryable<Photo> GetPhotoEntity(IPhotoRepository photoRepository) =>
            photoRepository.GetQueryable();
    }
}
