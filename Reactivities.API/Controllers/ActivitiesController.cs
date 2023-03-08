using API.Common;
using Application.Services.Activities;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reactivities.Application.Models.Activities;

namespace API.Controllers
{
    public class ActivitiesController : BaseApiController
    {
        private readonly IActivitiesServices _activitiesServices;

        public ActivitiesController(IActivitiesServices activitiesServices)
        {
            _activitiesServices = activitiesServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetActivities()
        {
            return HandleResult(await _activitiesServices.GetActivitiesAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetActivity(Guid id)
        {
            return HandleResult(await _activitiesServices.GetActivityDetailsAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateActivity(Activity activity)
        {
            return HandleResult(await _activitiesServices.CreateActivityAsync(activity));
        }

        [Authorize(Policy = "IsActivityHost")]
        [HttpPut("{id}")]
        public async Task<IActionResult> EditActivity(Guid id, Activity activity)
        {
            activity.Id = id;
            return HandleResult(await _activitiesServices.EditActivityAsync(activity));
        }

        [Authorize(Policy = "IsActivityHost")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivity(Guid id)
        {
            return HandleResult(await _activitiesServices.DeleteActivityAsync(id));
        }

        [HttpPost("{id}/Attend")]
        public async Task<IActionResult> Attend(Guid id)
        {
            return HandleResult(await _activitiesServices.UpdateAttendance(id));
        }
    }
}
