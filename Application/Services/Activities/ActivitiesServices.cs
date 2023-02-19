using Application.Queries.Activities;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Activities
{
    public class ActivitiesServices : IActivitiesServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IActivitiesQueryBuilder _activitiesQueryBuilder;
        public ActivitiesServices(IUnitOfWork unitOfWork,
                                  IActivitiesQueryBuilder activitiesQueryBuilder)
        {
            _unitOfWork = unitOfWork;
            _activitiesQueryBuilder = activitiesQueryBuilder;
        }

        public async Task<IEnumerable<Activity>> GetActivitiesAsync() => await _activitiesQueryBuilder.GetActivities(_unitOfWork.Activities)
                                                                                                      .ToListAsync();
    }
}
