using API.Common;
using Application.Services.Activities;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        //[HttpGet("{id}")]
        //public async Task<ActionResult<Activity>> GetActivity(Guid id)
        //{
        //    return Ok();
        //}
    }
}
