using Application.DTOs.Accounts;
using Reactivities.Domain.Interfaces;
using Reactivities.Domain.Models;

namespace Reactivities.Application.Queries.Followers
{
    public class FollowersQueryBuilder : IFollowersQueryBuilder
    {
        public IQueryable<UserFollowing> GetUserFollowingEntity(IUserFollowingsRepository userFollowingsRepository) =>
            userFollowingsRepository.GetQueryable();

        public IQueryable<ProfileDto> GetFollowersProfile(IUserFollowingsRepository userFollowingsRepository,
                                                          string username) =>
            SelectFollowersProfile(userFollowingsRepository.GetQueryable(a => a.Target.UserName == username), username);

        public IQueryable<ProfileDto> GetFollowingProfile(IUserFollowingsRepository userFollowingsRepository,
                                                          string username) =>
            SelectFollowingProfile(userFollowingsRepository.GetQueryable(a => a.Observer.UserName == username), username);

        private static IQueryable<ProfileDto> SelectFollowersProfile(IQueryable<UserFollowing> query, string username)
        {
            return query.Select(s => new ProfileDto
            {
                Username = s.Observer.UserName,
                DisplayName = s.Observer.DisplayName,
                Bio = s.Observer.Bio,
                Image = s.Observer.Photos.FirstOrDefault(a => a.IsMain).Url,
                FollowersCount = s.Observer.Followers.Count,
                FollowingCount = s.Observer.Followings.Count,
                Following = s.Observer.Followers.Any(a => a.Observer.UserName == username),
                Photos = s.Observer.Photos.Select(a => new Photo
                {
                    Id = a.Id,
                    PublicId = a.PublicId,
                    Url = a.Url,
                    IsMain = a.IsMain
                }).ToHashSet()
            });
        }

        private static IQueryable<ProfileDto> SelectFollowingProfile(IQueryable<UserFollowing> query, string username)
        {
            return query.Select(s => new ProfileDto
            {
                Username = s.Target.UserName,
                DisplayName = s.Target.DisplayName,
                Bio = s.Target.Bio,
                Image = s.Target.Photos.FirstOrDefault(a => a.IsMain).Url,
                FollowersCount = s.Target.Followers.Count,
                FollowingCount = s.Target.Followings.Count,
                Following = s.Target.Followers.Any(a => a.Observer.UserName == username),
                Photos = s.Target.Photos.Select(a => new Photo
                {
                    Id = a.Id,
                    PublicId = a.PublicId,
                    Url = a.Url,
                    IsMain = a.IsMain
                }).ToHashSet()
            });
        }
    }
}
