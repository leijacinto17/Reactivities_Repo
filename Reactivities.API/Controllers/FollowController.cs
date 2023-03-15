using API.Common;
using Microsoft.AspNetCore.Mvc;
using Reactivities.Application.Services.Followers;

namespace Reactivities.API.Controllers
{
    public class FollowController : BaseApiController
    {
        private readonly IFollowerServices _followServices;

        public FollowController(IFollowerServices followServices)
        {
            _followServices = followServices;
        }

        [HttpGet("{username}/GetFollowing")]
        public async Task<IActionResult> GetFollowing(string username, string predicate)
        {
            return HandleResult(await _followServices.FollowersListAsync(username,
                                                                         predicate));
        }

        [HttpPost("{username}/Follow")]
        public async Task<IActionResult> Follow(string username)
        {
            return HandleResult(await _followServices.FollowToggleAsync(username));
        }
    }
}
