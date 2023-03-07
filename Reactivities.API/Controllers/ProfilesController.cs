using API.Common;
using Microsoft.AspNetCore.Mvc;
using Reactivities.Application.Services.Profiles;

namespace Reactivities.API.Controllers
{
    public class ProfilesController : BaseApiController
    {
        private readonly IProfilesServices _profilesServices;

        public ProfilesController(IProfilesServices profilesServices)
        {
            _profilesServices = profilesServices;
        }

        [HttpGet("{username}")]
        public async Task<IActionResult> GetProfile(string username)
        {
            return HandleResult(await _profilesServices.GetProfileDetails(username));
        }
    }
}
