using Domain.Interfaces;
using Infrastructure.Repositories;

namespace Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Private class and Constructor
        private readonly DataContext _context;
        private readonly IActivitiesRepository _activitiesRepository;
        private readonly IUserRepository _userRepository;

        public UnitOfWork(DataContext context)
        {
            _context = context;
        }
        #endregion

        public IActivitiesRepository Activities => _activitiesRepository
                                                   ?? new ActivitiesRepository(_context);

        public IUserRepository Users => _userRepository
                                       ?? new UserRepository(_context);

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
