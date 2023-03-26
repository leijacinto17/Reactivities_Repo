using Domain.Interfaces;
using Infrastructure.Repositories;
using Reactivities.Domain.Interfaces;
using Reactivities.Persistence.Repositories;

namespace Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Private class and Constructor
        private readonly DataContext _context;
        private readonly IActivitiesRepository _activitiesRepository;
        private readonly IUserRepository _userRepository;
        private readonly IPhotoRepository _photoRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly IUserFollowingsRepository _userFollowingsRepository;
        private readonly IActivityAttendeeRepository _activityAttendeeRepository;

        public UnitOfWork(DataContext context)
        {
            _context = context;
        }
        #endregion

        public IActivitiesRepository Activities => _activitiesRepository
                                                   ?? new ActivitiesRepository(_context);

        public IUserRepository Users => _userRepository
                                       ?? new UserRepository(_context);

        public IPhotoRepository Photos => _photoRepository
                                          ?? new PhotoRepository(_context);

        public ICommentRepository Comments => _commentRepository
                                         ?? new CommentRepository(_context);

        public IUserFollowingsRepository UserFollowings => _userFollowingsRepository
                                                           ?? new UserFollowingsRepository(_context);

        public IActivityAttendeeRepository ActivityAttendee => _activityAttendeeRepository
                                                               ?? new ActivityAttendeeRepository(_context);

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
