using API.Common;
using Application.DTOs.Accounts;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Reactivities.Application.Models.Profiles;
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

        [HttpPut("{username}/EditAbout")]
        public async Task<IActionResult> EditAbout(string username, UserAboutValues message)
        {
            return HandleResult(await _profilesServices.EditUserAbout(username, message));
        }
    }
}
