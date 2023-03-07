using Application.DTOs.Accounts;
using Domain.Interfaces;
using Domain.Models;

namespace Application.Queries.Users
{
    public interface IUserQueryBuilder
    {
        IQueryable<ProfileDto> GetProfileDetails(IUserRepository userRepository);
        IQueryable<User> GetUserEntity(IUserRepository userRepository);
    }
}