using Application.Queries.Activities;
using AutoMapper;
using Domain.Interfaces;
using Domain.Models;
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

        public async Task<IEnumerable<Activity>> GetActivitiesAsync() => await _activitiesQueryBuilder.GetActivities(_unitOfWork.Activities)
                                                                                                      .ToListAsync();

        public async Task<Activity> GetActivityDetailsAsync(Guid id) => await _activitiesQueryBuilder.GetActivities(_unitOfWork.Activities)
                                                                                                .FirstOrDefaultAsync(a => a.Id == id);

        public async Task<Activity> CreateActivityAsync(Activity activity)
        {
            var newAcitivity = await _unitOfWork.Activities.Insert(activity);
            await _unitOfWork.SaveChangesAsync();

            return newAcitivity;
        }

        public async Task<Activity> EditActivityAsync(Activity message)
        {
            var activity = await _activitiesQueryBuilder.GetActivities(_unitOfWork.Activities)
                                                        .FirstOrDefaultAsync(a => a.Id == message.Id);

            _mapper.Map(message, activity);

            await _unitOfWork.SaveChangesAsync();

            return activity;
        }

        public async Task<IEnumerable<Activity>> DeleteActivityAsync(Guid id)
        {
            var activity = await _activitiesQueryBuilder.GetActivities(_unitOfWork.Activities)
                                                        .FirstOrDefaultAsync(a => a.Id == id);

            _unitOfWork.Activities.Delete(activity);

            await _unitOfWork.SaveChangesAsync();

            return await GetActivitiesAsync();
        }
    }
}
