using Application.Core;
using Application.Queries.Activities;
using Application.Validators.Activities;
using AutoMapper;
using Domain.Interfaces;
using Domain.Models;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Activities
{
    public class ActivitiesServices : IActivitiesServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IActivitiesQueryBuilder _activitiesQueryBuilder;
        private readonly IMapper _mapper;
        public ActivitiesServices(IUnitOfWork unitOfWork,
                                  IActivitiesQueryBuilder activitiesQueryBuilder,
                                  IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _activitiesQueryBuilder = activitiesQueryBuilder;
            _mapper = mapper;
        }

        public class CommandValidator : AbstractValidator<Activity>
        {
            public CommandValidator()
            {
                RuleFor(x => x).SetValidator(new ActivityValidators());
            }
        }

        public async Task<Result<IEnumerable<Activity>>> GetActivitiesAsync()
        {
            var activities = await _activitiesQueryBuilder.GetActivities(_unitOfWork.Activities)
                                                          .ToListAsync();

            return Result<IEnumerable<Activity>>.Success(activities);
        }

        public async Task<Result<Activity>> GetActivityDetailsAsync(Guid id)
        {
            var activity = await _activitiesQueryBuilder.GetActivities(_unitOfWork.Activities)
                                                        .FirstOrDefaultAsync(a => a.Id == id);
            return Result<Activity>.Success(activity);
        }

        public async Task<Result<Activity>> CreateActivityAsync(Activity activity)
        {
            var newAcitivity = await _unitOfWork.Activities.Insert(activity);
            var result = await _unitOfWork.SaveChangesAsync();

            if (!result)
                return Result<Activity>.Failure("Failed to create Activity");

            return Result<Activity>.Success(activity);
        }

        public async Task<Result<Activity>> EditActivityAsync(Activity message)
        {
            var activity = await _activitiesQueryBuilder.GetActivities(_unitOfWork.Activities)
                                                        .FirstOrDefaultAsync(a => a.Id == message.Id);

            if (activity == null) return null;

            _mapper.Map(message, activity);

            var result = await _unitOfWork.SaveChangesAsync();

            if (!result) return Result<Activity>.Failure("Failed to update activity");

            return Result<Activity>.Success(activity);
        }

        public async Task<Result<IEnumerable<Activity>>> DeleteActivityAsync(Guid id)
        {
            var activity = await _activitiesQueryBuilder.GetActivities(_unitOfWork.Activities)
                                                        .FirstOrDefaultAsync(a => a.Id == id);

            if (activity == null) return null;

            _unitOfWork.Activities.Delete(activity);

            var result = await _unitOfWork.SaveChangesAsync();

            if (!result) return Result<IEnumerable<Activity>>.Failure("Failed to delete activity");

            var activities = await GetActivitiesAsync();

            return activities;
        }
    }
}
