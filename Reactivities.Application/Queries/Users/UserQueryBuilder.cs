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

        public IQueryable<ProfileDto> GetProfileDetails(IUserRepository userRepository) =>
            SelectProfileDto(userRepository.GetQueryable());

        
        private IQueryable<ProfileDto> SelectProfileDto(IQueryable<User> users)
        {
            return users.Select(a => new ProfileDto
            {
                Username = a.UserName,
                DisplayName = a.DisplayName,
                Bio = a.Bio,
                Image = a.Photos.FirstOrDefault(a => a.IsMain).Url,
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
