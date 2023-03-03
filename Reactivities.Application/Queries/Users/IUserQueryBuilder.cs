using Domain.Interfaces;
using Domain.Models;

namespace Application.Queries.Users
{
    public interface IUserQueryBuilder
    {
        IQueryable<User> GetUserEntity(IUserRepository userRepository);
    }
}