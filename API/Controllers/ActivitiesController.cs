using API.Common;
using Application.Services.Activities;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPut]
        public async Task<IActionResult> EditActivity(Activity activity)
        {
            return HandleResult(await _activitiesServices.EditActivityAsync(activity));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivity(Guid id)
        {
            return HandleResult(await _activitiesServices.DeleteActivityAsync(id));
        }
    }
}
