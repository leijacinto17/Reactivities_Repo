using Application.DTOs.Accounts;
using Domain.Interfaces;
using Domain.Models;
using Reactivities.Domain.Models;

namespace Application.Queries.Users
{
    public class UserQueryBuilder : IUserQueryBuilder
    {
        public IQueryable<User> GetUserEntity(IUserRepository userRepository) =>
            userRepository.GetQueryable();

        public IQueryable<ProfileDto> GetProfileDetails(IUserRepository userRepository,
                                                        string username) =>
            SelectProfileDto(userRepository.GetQueryable(), username);
        
        private static IQueryable<ProfileDto> SelectProfileDto(IQueryable<User> users, string username)
        {
            return users.Select(a => new ProfileDto
            {
                Username = a.UserName,
                DisplayName = a.DisplayName,
                Bio = a.Bio,
                Image = a.Photos.FirstOrDefault(a => a.IsMain).Url,
                FollowersCount = a.Followers.Count,
                FollowingCount = a.Followings.Count,
                Following = a.Followers.Any(s => s.Observer.UserName == username),
                Photos = a.Photos.Select(a => new Photo
                {
                    Id = a.Id,
                    PublicId = a.PublicId,
                    Url = a.Url,
                    IsMain = a.IsMain,
                }).ToHashSet()
            });
        }
    }
}
