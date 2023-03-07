using Application.Core;
using Application.DTOs.Accounts;
using Application.Queries.Users;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Reactivities.Application.Services.Profiles
{
    public  class ProfilesServices : IProfilesServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserQueryBuilder _userQueryBuilder;

        public ProfilesServices(IUserQueryBuilder userQueryBuilder,
                                IUnitOfWork unitOfWork)
        {
            _userQueryBuilder = userQueryBuilder;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<ProfileDto>> GetProfileDetails(string username)
        {
            var user = await _userQueryBuilder.GetProfileDetails(_unitOfWork.Users)
                                              .FirstOrDefaultAsync(a => a.Username == username);

            if (user == null) return null;

            return Result<ProfileDto>.Success(user);
        }
    }
}
