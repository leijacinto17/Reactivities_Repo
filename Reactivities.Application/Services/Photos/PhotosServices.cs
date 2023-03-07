using Application.Core;
using Application.Interfaces;
using Application.Queries.Users;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Reactivities.Application.Interfaces;
using Reactivities.Application.Queries.Photos;
using Reactivities.Domain.Models;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

namespace Reactivities.Application.Services.Photos
{
    public class PhotosServices : IPhotosServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPhotosQueryBuilder _photosQueryBuilder;
        private readonly IUserQueryBuilder _userQueryBuilder;
        private readonly IPhotoAccessor _photoAccessor;
        private readonly IUserAccessor _userAccessor;

        public PhotosServices(IPhotosQueryBuilder photosQueryBuilder,
                              IPhotoAccessor photoAccessor,
                              IUserAccessor userAccessor,
                              IUserQueryBuilder userQueryBuilder,
                              IUnitOfWork unitOfWork)
        {
            _photosQueryBuilder = photosQueryBuilder;
            _photoAccessor = photoAccessor;
            _userAccessor = userAccessor;
            _userQueryBuilder = userQueryBuilder;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<IEnumerable<Photo>>> GetPhotoListAsync()
        {
            var user = await _userQueryBuilder.GetUserEntity(_unitOfWork.Users)
                                             .FirstOrDefaultAsync(a => a.UserName == _userAccessor.GetUername());

            if (user == null) return null;

            var result = user.Photos;

            return result != null ? Result<IEnumerable<Photo>>.Success(result) : null;
        }

        public async Task<Result<Photo>> GetPhotoDetails(string publicId)
        {
            var user = await _userQueryBuilder.GetUserEntity(_unitOfWork.Users)
                                             .FirstOrDefaultAsync(a => a.UserName == _userAccessor.GetUername());

            if (user == null) return null;

            var result = user.Photos.FirstOrDefault(a => a.PublicId == publicId);

            if (result == null) Result<Photo>.Failure("Photo not found");

            return Result<Photo>.Success(result);
        }

        public async Task<Result<Photo>> InsertPhotoAsync(IFormFile file)
        {
            var user = await _userQueryBuilder.GetUserEntity(_unitOfWork.Users)
                                              .FirstOrDefaultAsync(a => a.UserName == _userAccessor.GetUername());

            if (user == null) return null;

            var photoUploadResult = await _photoAccessor.AddPhoto(file);

            var photo = new Photo
            {
                Url = photoUploadResult.Url,
                PublicId = photoUploadResult.PublicId
            };

            if (!user.Photos.Any(a => a.IsMain)) photo.IsMain = true;

            user.Photos.Add(photo);

            var result = await _unitOfWork.SaveChangesAsync();

            return result ? Result<Photo>.Success(photo) : Result<Photo>.Failure("Problem in adding photo");
        }

        public async Task<Result<IEnumerable<Photo>>> DeletePhotoAsync(string publicId)
        {
            var user = await _userQueryBuilder.GetUserEntity(_unitOfWork.Users)
                                              .FirstOrDefaultAsync(a => a.UserName == _userAccessor.GetUername());

            if (user == null) return null;

            var photo = user.Photos.FirstOrDefault(a => a.PublicId == publicId);

            if (photo == null) return null;

            if (photo.IsMain) return Result<IEnumerable<Photo>>.Failure("You cannot delete your main photo");

            var result = await _photoAccessor.DeletePhoto(photo.PublicId);

            if (result == null) return Result<IEnumerable<Photo>>.Failure("Problem deleting photo from storage");

            _unitOfWork.Photos.Delete(photo);

            var success = await _unitOfWork.SaveChangesAsync();

            return success ? await GetPhotoListAsync() : Result<IEnumerable<Photo>>.Failure("Problem deleting photo");
        }

        public async Task<Result<Photo>> SetMainPhotoAsync(string publicId)
        {
            var user = await _userQueryBuilder.GetUserEntity(_unitOfWork.Users)
                                             .FirstOrDefaultAsync(a => a.UserName == _userAccessor.GetUername());

            if (user == null) return null;

            var photo = user.Photos.FirstOrDefault(x => x.PublicId == publicId);

            if (photo == null) return null;

            var currentMainPhoto = user.Photos.FirstOrDefault(x => x.IsMain);

            if (currentMainPhoto != null) currentMainPhoto.IsMain = false;

            photo.IsMain = true;

            var success = await _unitOfWork.SaveChangesAsync();

            return success ? Result<Photo>.Success(photo) 
                : Result<Photo>.Failure("Problem changing main photo");
        }
    }
}
