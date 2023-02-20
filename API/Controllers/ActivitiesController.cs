using API.Common;
using Application.Services.Activities;
using Domain.Models;
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
        public async Task<IEnumerable<Activity>> GetActivities()
        {
            return await _activitiesServices.GetActivitiesAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Activity>> GetActivity(Guid id)
        {
            return await _activitiesServices.GetActivityDetailsAsync(id);
        }

        [HttpPost]
        public async Task<IActionResult> CreateActivity(Activity activity)
        {
            return Ok(await _activitiesServices.CreateActivityAsync(activity));
        }

        [HttpPut]
        public async Task<IActionResult> EditActivity(Activity activity)
        {
            return Ok(await _activitiesServices.EditActivityAsync(activity));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivity(Guid id)
        {
            return Ok(await _activitiesServices.DeleteActivityAsync(id));
        }
    }
}
