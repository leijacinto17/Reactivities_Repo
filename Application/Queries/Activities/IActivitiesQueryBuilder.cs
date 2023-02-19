using Domain.Interfaces;
using Domain.Models;

namespace Application.Queries.Activities
{
    public interface IActivitiesQueryBuilder
    {
        IQueryable<Activity> GetActivities(IActivitiesRepository activitiesRepository);
    }
}
