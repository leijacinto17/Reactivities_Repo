using API.Common;
using Application.DTOs.Accounts;
using Application.Services.Activities;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Reactivities.Application.Models.Profiles;
using Reactivities.Application.Services.Profiles;

namespace Reactivities.API.Controllers
{
    public class ProfilesController : BaseApiController
    {
        private readonly IProfilesServices _profilesServices;
        private readonly IActivitiesServices _activitiesServices;

        public ProfilesController(IProfilesServices profilesServices,
                                  IActivitiesServices activitiesServices)
        {
            _profilesServices = profilesServices;
            _activitiesServices = activitiesServices;
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

        #region List of Activity
        [HttpGet("{username}/Activities")]
        public async Task<IActionResult> Activities(string username, string predicate)
        {
            return HandleResult(await _activitiesServices.GetUserActivityAsync(username,
                                                                               predicate));
        }
        #endregion
    }
}
