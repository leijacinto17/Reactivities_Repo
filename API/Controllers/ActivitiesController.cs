using API.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class ActivitiesController : BaseApiController
    {
        private readonly IMediator _mediator;
        public ActivitiesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //[HttpGet]
        //public async Task<IEnumerable<Activity>> GetActivities()
        //{
        //    return await _mediator.Send(new List.Query());
        //}

        //[HttpGet("{id}")]
        //public async Task<ActionResult<Activity>> GetActivity(Guid id)
        //{
        //    return Ok();
        //}
    }
}
