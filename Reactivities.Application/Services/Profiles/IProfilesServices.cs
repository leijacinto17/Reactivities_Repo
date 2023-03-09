using Application.Core;
using Application.DTOs.Accounts;
using Reactivities.Application.Models.Profiles;

namespace Reactivities.Application.Services.Profiles
{
    public interface IProfilesServices
    {
        Task<Result<ProfileDto>> EditUserAbout(string username, UserAboutValues message);
        Task<Result<ProfileDto>> GetProfileDetails(string username);
    }
}