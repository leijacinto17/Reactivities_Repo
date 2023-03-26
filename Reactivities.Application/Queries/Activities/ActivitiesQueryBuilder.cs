using Application.DTOs.Activities;
using Domain.Interfaces;
using Domain.Models;
using Reactivities.Application.DTOs.Accounts;
using Reactivities.Application.DTOs.Activities;
using Reactivities.Application.Models.Activities;
using Reactivities.Domain.Interfaces;

namespace Application.Queries.Activities
{
    public class ActivitiesQueryBuilder : IActivitiesQueryBuilder
    {
        public IQueryable<Activity> GetActivityEntity(IActivitiesRepository activitiesRepository) =>
            activitiesRepository.GetQueryable();

        public IQueryable<ActivityDto> GetActivities(IActivitiesRepository activitiesRepository,
                                                     string username,
                                                     ActivityParams param) =>
            SelectActivities(Filter(activitiesRepository.GetQueryable(),
                                    username,
                                    param), username);

        public IQueryable<UserActivityDto> GetUserActivity(IActivityAttendeeRepository activityAttendeeRepository,
                                                           string username) =>
            SelectUserActivities(activityAttendeeRepository.GetQueryable(a => a.User.UserName == username));

        private IQueryable<Activity> Filter(IQueryable<Activity> query,
                                            string username,
                                            ActivityParams param)
        {
            query = query.Where(s => s.Date >= param.StartDate);

            if (param.IsGoing && !param.IsHost)
                query = query.Where(s => s.Attendees.Any(a => a.User.UserName == username));

            if (param.IsHost && !param.IsGoing)
                query = query.Where(s => s.Attendees.FirstOrDefault(w => w.IsHost).User.UserName == username);

            return query;
        }

        private static IQueryable<ActivityDto> SelectActivities(IQueryable<Activity> query, string username)
        {
            return query.Select(s => new ActivityDto
            {
                Id = s.Id,
                Title = s.Title,
                Date = s.Date,
                Description = s.Description,
                Category = s.Category,
                City = s.City,
                Venue = s.Venue,
                HostUsername = s.Attendees.FirstOrDefault(a => a.IsHost).User.UserName,
                IsCancelled = s.IsCancelled,
                Attendees = s.Attendees.Select(a => new AttendeesDto
                {
                    Username = a.User.UserName,
                    DisplayName = a.User.DisplayName,
                    Bio = a.User.Bio,
                    Image = a.User.Photos.FirstOrDefault(x => x.IsMain).Url,
                    FollowersCount = a.User.Followers.Count,
                    FollowingCount = a.User.Followings.Count,
                    Following = a.User.Followers.Any(x => x.Observer.UserName == username)
                }).ToHashSet(),
            });
        }

        private static IQueryable<UserActivityDto> SelectUserActivities(IQueryable<ActivityAttendee> query)
        {
            return query.Select(s => new UserActivityDto
            {
                Id = s.Activity.Id,
                Title = s.Activity.Title,
                Category = s.Activity.Category,
                Date = s.Activity.Date,
                HostUsername = s.Activity.Attendees.FirstOrDefault(a => a.IsHost).User.UserName
            });
        }
    }
}
