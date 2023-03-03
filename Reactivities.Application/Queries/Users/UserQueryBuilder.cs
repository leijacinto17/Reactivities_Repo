using Domain.Interfaces;
using Domain.Models;

namespace Application.Queries.Users
{
    public class UserQueryBuilder : IUserQueryBuilder
    {
        public IQueryable<User> GetUserEntity(IUserRepository userRepository) =>
            userRepository.GetQueryable();
    }
}
