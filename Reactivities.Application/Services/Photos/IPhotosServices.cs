using Application.Core;
using Microsoft.AspNetCore.Http;
using Reactivities.Domain.Models;

namespace Reactivities.Application.Services.Photos
{
    public interface IPhotosServices
    {
        Task<Result<IEnumerable<Photo>>> DeletePhotoAsync(string publicId);
        Task<Result<Photo>> GetPhotoDetails(string publicId);
        Task<Result<IEnumerable<Photo>>> GetPhotoListAsync();
        Task<Result<Photo>> InsertPhotoAsync(IFormFile file);
        Task<Result<Photo>> SetMainPhotoAsync(string publicId);
    }
}