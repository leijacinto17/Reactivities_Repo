using Reactivities.Domain.Interfaces;

namespace Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IActivitiesRepository Activities { get; }
        IUserRepository Users { get; }
        IPhotoRepository Photos { get; }
        ICommentRepository Comments { get; }
        IUserFollowingsRepository UserFollowings { get; }
        IActivityAttendeeRepository ActivityAttendee { get; }
        Task<bool> SaveChangesAsync();
    }
}