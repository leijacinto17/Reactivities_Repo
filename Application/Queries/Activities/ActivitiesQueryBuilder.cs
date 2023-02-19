using Domain.Interfaces;
using Domain.Models;

namespace Application.Queries.Activities
{
    public class ActivitiesQueryBuilder : IActivitiesQueryBuilder
    {
        public IQueryable<Activity> GetActivities(IActivitiesRepository activitiesRepository) =>
            activitiesRepository.GetQueryable();
    }
}
