using Application.DTOs.Activities;
using Domain.Interfaces;
using Domain.Models;
using Reactivities.Application.DTOs.Accounts;
using Reactivities.Application.Models.Activities;
using Reactivities.Domain.Interfaces;

namespace Application.Queries.Activities
{
    public interface IActivitiesQueryBuilder
    {
        IQueryable<ActivityDto> GetActivities(IActivitiesRepository activitiesRepository,
                                              string username,
                                              ActivityParams param);
        IQueryable<Activity> GetActivityEntity(IActivitiesRepository activitiesRepository);
        IQueryable<UserActivityDto> GetUserActivity(IActivityAttendeeRepository activityAttendeeRepository,
                                                    string username);
    }
}
