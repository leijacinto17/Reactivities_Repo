using Application.Core;
using Application.DTOs.Accounts;
using Application.Interfaces;
using Application.Queries.Users;
using Domain.Interfaces;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Reactivities.Application.Models.Profiles;

namespace Reactivities.Application.Services.Profiles
{
    public  class ProfilesServices : IProfilesServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserQueryBuilder _userQueryBuilder;
        private readonly IUserAccessor _userAccessor;

        public ProfilesServices(IUserQueryBuilder userQueryBuilder,
                                IUnitOfWork unitOfWork,
                                IUserAccessor userAccessor)
        {
            _userQueryBuilder = userQueryBuilder;
            _unitOfWork = unitOfWork;
            _userAccessor = userAccessor;
        }

        public async Task<Result<ProfileDto>> GetProfileDetails(string username)
        {
            var user = await _userQueryBuilder.GetProfileDetails(_unitOfWork.Users, _userAccessor.GetUername())
                                              .FirstOrDefaultAsync(a => a.Username == username);

            if (user == null) return null;

            return Result<ProfileDto>.Success(user);
        }

        public async Task<Result<ProfileDto>> EditUserAbout(string username, UserAboutValues message)
        {
            var currentUser = await _userQueryBuilder.GetUserEntity(_unitOfWork.Users)
                                             .FirstOrDefaultAsync(a => a.UserName == _userAccessor.GetUername());

            if (currentUser == null) return null;

            if (currentUser.UserName != username) return Result<ProfileDto>.Failure("You are not authorize to update other info");

            if (currentUser.DisplayName != message.DisplayName)
                currentUser.DisplayName = message.DisplayName;

            currentUser.Bio = message.Bio;

            var result = await _unitOfWork.SaveChangesAsync();

            return result ? await GetProfileDetails(username) : Result<ProfileDto>.Failure("Problem in updating user info");
        }
    }
}
