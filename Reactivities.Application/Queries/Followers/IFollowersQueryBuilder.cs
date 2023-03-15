using Application.DTOs.Accounts;
using Reactivities.Domain.Interfaces;
using Reactivities.Domain.Models;

namespace Reactivities.Application.Queries.Followers
{
    public interface IFollowersQueryBuilder
    {
        IQueryable<ProfileDto> GetFollowersProfile(IUserFollowingsRepository userFollowingsRepository, string username);
        IQueryable<ProfileDto> GetFollowingProfile(IUserFollowingsRepository userFollowingsRepository, string username);
        IQueryable<UserFollowing> GetUserFollowingEntity(IUserFollowingsRepository userFollowingsRepository);
    }
}