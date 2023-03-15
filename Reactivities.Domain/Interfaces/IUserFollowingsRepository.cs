using Domain.Interfaces;
using Reactivities.Domain.Models;

namespace Reactivities.Domain.Interfaces
{
    public interface IUserFollowingsRepository : IGenericRepository<UserFollowing>
    {
        Task<UserFollowing> Following(string user, string targetUser);
    }
}
