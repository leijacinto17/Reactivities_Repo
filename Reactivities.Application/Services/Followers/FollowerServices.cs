using Application.Core;
using Application.DTOs.Accounts;
using Application.Interfaces;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Reactivities.Application.Queries.Followers;
using Reactivities.Domain.Models;

namespace Reactivities.Application.Services.Followers
{
    public class FollowerServices : IFollowerServices
    {
        private readonly IUserAccessor _userAccessor;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFollowersQueryBuilder _followersQueryBuilder;

        public FollowerServices(IUnitOfWork unitOfWork, IUserAccessor userAccessor, IFollowersQueryBuilder followersQueryBuilder)
        {
            _unitOfWork = unitOfWork;
            _userAccessor = userAccessor;
            _followersQueryBuilder = followersQueryBuilder;
        }

        public async Task<Result<IEnumerable<ProfileDto>>> FollowersListAsync(string username, string predicate)
        {
            var profiles = new List<ProfileDto>();

            switch(predicate) 
            {
                case "followers":
                    profiles = await _followersQueryBuilder.GetFollowersProfile(_unitOfWork.UserFollowings, username)
                                                           .ToListAsync();
                    break;
                case "following":
                    profiles = await _followersQueryBuilder.GetFollowingProfile(_unitOfWork.UserFollowings, username)
                                                           .ToListAsync(); 
                    break;
            }

            return Result<IEnumerable<ProfileDto>>.Success(profiles);
        }

        public async Task<Result<bool>> FollowToggleAsync(string targetUsername)
        {
            var observer = await _unitOfWork.Users.GetQueryable()
                                                  .FirstOrDefaultAsync(a => a.UserName == _userAccessor.GetUername());

            var target = await _unitOfWork.Users.GetQueryable()
                                                .FirstOrDefaultAsync(a => a.UserName == targetUsername);

            if (target == null) return null;

            var following = await _unitOfWork.UserFollowings.Following(observer.Id, target.Id);

            if (following == null)
            {
                following = new UserFollowing
                {
                    Observer = observer,
                    Target = target
                };
                await _unitOfWork.UserFollowings.InsertAsync(following);
            }
            else
            {
                _unitOfWork.UserFollowings.Delete(following);
            }

            var success = await _unitOfWork.SaveChangesAsync();

            return success
                ? Result<bool>.Success(true)
                : Result<bool>.Failure("Failed to update following");
        }
    }
}
