using Application.Core;
using Application.DTOs.Activities;
using Application.Interfaces;
using Application.Queries.Activities;
using Application.Queries.Users;
using Application.Validators.Activities;
using Domain.Interfaces;
using Domain.Models;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Reactivities.Application.Core;

namespace Application.Services.Activities
{
    public class ActivitiesServices : IActivitiesServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IActivitiesQueryBuilder _activitiesQueryBuilder;
        private readonly IUserAccessor _userAccessor;
        private readonly IUserQueryBuilder _userQueryBuilder;
        public ActivitiesServices(IUnitOfWork unitOfWork,
                                  IActivitiesQueryBuilder activitiesQueryBuilder,
                                  IUserAccessor userAccessor,
                                  IUserQueryBuilder userQueryBuilder)
        {
            _unitOfWork = unitOfWork;
            _activitiesQueryBuilder = activitiesQueryBuilder;
            _userAccessor = userAccessor;
            _userQueryBuilder = userQueryBuilder;
        }

        public class CommandValidator : AbstractValidator<Activity>
        {
            public CommandValidator()
            {
                RuleFor(x => x).SetValidator(new ActivityValidators());
            }
        }

        #region Activities Methods
        public async Task<Result<PageList<ActivityDto>>> GetActivitiesAsync(PagingParams pagingParams)
        {
            var activities = _activitiesQueryBuilder.GetActivities(_unitOfWork.Activities,
                                                                   _userAccessor.GetUername())
                                                    .OrderBy(a => a.Date);

            var result = await PageList<ActivityDto>.CreateAsync(activities,
                                                                 pagingParams.PageNumber,
                                                                 pagingParams.PageSize);

            return Result<PageList<ActivityDto>>.Success(result);
        }

        public async Task<Result<ActivityDto>> GetActivityDetailsAsync(Guid id)
        {
            var activity = await _activitiesQueryBuilder.GetActivities(_unitOfWork.Activities, _userAccessor.GetUername())
                                                        .FirstOrDefaultAsync(a => a.Id == id);
            return Result<ActivityDto>.Success(activity);
        }

        public async Task<Result<ActivityDto>> CreateActivityAsync(Activity activity)
        {
            var user = await _userQueryBuilder.GetUserEntity(_unitOfWork.Users)
                                              .FirstOrDefaultAsync(a => a.UserName == _userAccessor.GetUername());

            var attendee = new ActivityAttendee
            {
                User = user,
                Activity = activity,
                IsHost = true
            };

            activity.Attendees.Add(attendee);

            await _unitOfWork.Activities.InsertAsync(activity);
            var result = await _unitOfWork.SaveChangesAsync();

            if (!result)
                return Result<ActivityDto>.Failure("Failed to create Activity");

            return await GetActivityDetailsAsync(activity.Id);
        }

        public async Task<Result<ActivityDto>> EditActivityAsync(Activity message)
        {
            var activity = await _activitiesQueryBuilder.GetActivityEntity(_unitOfWork.Activities)
                                                        .FirstOrDefaultAsync(a => a.Id == message.Id);

            if (activity == null) return null;

            activity.Title = message.Title;
            activity.Date = message.Date;
            activity.Description = message.Description;
            activity.Category = message.Category;
            activity.City = message.City;
            activity.Venue = message.Venue;

            var result = await _unitOfWork.SaveChangesAsync();

            if (!result) return Result<ActivityDto>.Failure("Failed to update activity");

            return await GetActivityDetailsAsync(activity.Id);
        }

        public async Task<Result<bool>> DeleteActivityAsync(Guid id)
        {
            var activity = await _activitiesQueryBuilder.GetActivityEntity(_unitOfWork.Activities)
                                                        .FirstOrDefaultAsync(a => a.Id == id);

            if (activity == null) return null;

            _unitOfWork.Activities.Delete(activity);

            var result = await _unitOfWork.SaveChangesAsync();

            if (!result) return Result<bool>.Failure("Failed to delete activity");

            return Result<bool>.Success(true);
        }
        #endregion

        #region Attendee Methods
        public async Task<Result<ActivityDto>> UpdateAttendance(Guid id)
        {
            var activity = await _activitiesQueryBuilder.GetActivityEntity(_unitOfWork.Activities)
                                                        .SingleOrDefaultAsync(a => a.Id == id);

            if (activity == null) return null;

            var user = await _userQueryBuilder.GetUserEntity(_unitOfWork.Users)
                                              .FirstOrDefaultAsync(a => a.UserName == _userAccessor.GetUername());

            if (user == null) return null;

            var hostUsername = activity.Attendees.FirstOrDefault(a => a.IsHost)?.User?.UserName;

            var attendance = activity.Attendees.FirstOrDefault(a => a.User.UserName == user.UserName);

            if (attendance != null && hostUsername == user.UserName)
                activity.IsCancelled = !activity.IsCancelled;

            if (attendance != null && hostUsername != user.UserName)
                activity.Attendees.Remove(attendance);

            if (attendance == null)
            {
                attendance = new ActivityAttendee
                {
                    User = user,
                    Activity = activity,
                    IsHost = false
                };

                activity.Attendees.Add(attendance);
            }

            var result = await _unitOfWork.SaveChangesAsync();

            return result ? await GetActivityDetailsAsync(id) : Result<ActivityDto>.Failure("Problem updating attendance");
        }
        #endregion
    }
}
