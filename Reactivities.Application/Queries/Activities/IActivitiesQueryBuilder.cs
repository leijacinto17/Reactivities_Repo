using Application.DTOs.Activities;
using Domain.Interfaces;
using Domain.Models;

namespace Application.Queries.Activities
{
    public interface IActivitiesQueryBuilder
    {
        IQueryable<ActivityDto> GetActivities(IActivitiesRepository activitiesRepository);
        IQueryable<Activity> GetActivityEntity(IActivitiesRepository activitiesRepository);
    }
}
