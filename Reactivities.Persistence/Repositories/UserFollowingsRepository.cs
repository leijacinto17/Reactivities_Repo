using Infrastructure;
using Infrastructure.Repositories;
using Reactivities.Domain.Interfaces;
using Reactivities.Domain.Models;

namespace Reactivities.Persistence.Repositories
{
    public class UserFollowingsRepository : GenericRepository<UserFollowing>, IUserFollowingsRepository
    {
        private readonly DataContext _dataContext;
        public UserFollowingsRepository(DataContext context) : base(context)
        {
            _dataContext = context;
        }

        public async Task<UserFollowing> Following(string user, string targetUser)
        {
            return await _dataContext.UserFollowings.FindAsync(user, targetUser);
        }
    }
}
