using Application.Core;
using Application.DTOs.Accounts;

namespace Reactivities.Application.Services.Profiles
{
    public interface IProfilesServices
    {
        Task<Result<ProfileDto>> GetProfileDetails(string username);
    }
}