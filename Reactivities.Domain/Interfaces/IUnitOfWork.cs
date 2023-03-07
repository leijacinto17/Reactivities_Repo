using Reactivities.Domain.Interfaces;

namespace Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IActivitiesRepository Activities { get; }
        IUserRepository Users { get; }
        IPhotoRepository Photos { get; }
        Task<bool> SaveChangesAsync();
    }
}