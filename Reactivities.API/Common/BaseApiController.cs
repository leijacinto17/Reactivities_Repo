using Application.Core;
using Microsoft.AspNetCore.Mvc;
using Reactivities.API.Extensions;
using Reactivities.Application.Core;

namespace API.Common
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {

        protected ActionResult HandleResult<T>(Result<T> result)
        {
            if (result == null)
                return NotFound();
            if (result.IsSuccess && result.Value != null)
                return Ok(result.Value);
            if (result.IsSuccess && result.Value == null)
                return NotFound();

            return BadRequest(result.Error);
        }

        protected ActionResult HandlePagedResult<T>(Result<PageList<T>> result)
        {
            if (result == null)
                return NotFound();
            if (result.IsSuccess && result.Value != null)
            {
                Response.AddPaginationHeader(result.Value.CurrentPage,
                                             result.Value.PageSize,
                                             result.Value.TotalCount,
                                             result.Value.TotalPages);
                return Ok(result.Value);
            }
            if (result.IsSuccess && result.Value == null)
                return NotFound();

            return BadRequest(result.Error);
        }
    }
}
