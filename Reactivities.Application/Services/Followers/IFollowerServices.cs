using Application.Core;
using Application.DTOs.Accounts;

namespace Reactivities.Application.Services.Followers
{
    public interface IFollowerServices
    {
        Task<Result<IEnumerable<ProfileDto>>> FollowersListAsync(string username, string predicate);
        Task<Result<bool>> FollowToggleAsync(string targetUsername);
    }
}