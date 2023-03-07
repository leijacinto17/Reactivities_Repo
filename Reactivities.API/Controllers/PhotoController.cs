using API.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reactivities.Application.Services.Photos;

namespace Reactivities.API.Controllers
{
    public class PhotoController : BaseApiController
    {
        private readonly IPhotosServices _photosServices;

        public PhotoController(IPhotosServices photosServices)
        {
            _photosServices = photosServices;
        }

        [HttpPost]
        public async Task<IActionResult> InsertPhoto([FromForm] IFormFile File)
        {
            return HandleResult(await _photosServices.InsertPhotoAsync(File));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePhoto(string id)
        {
            return HandleResult(await _photosServices.DeletePhotoAsync(id));
        }

        [HttpPost("{id}/SetMainPhoto")]
        public async Task<IActionResult> SetMainPhoto(string id)
        {
            return HandleResult(await _photosServices.SetMainPhotoAsync(id));
        }
    }
}
