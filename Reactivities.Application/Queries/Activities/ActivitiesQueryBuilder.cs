using Application.DTOs.Activities;
using Domain.Interfaces;
using Domain.Models;
using Reactivities.Application.DTOs.Activities;

namespace Application.Queries.Activities
{
    public class ActivitiesQueryBuilder : IActivitiesQueryBuilder
    {
        public IQueryable<Activity> GetActivityEntity(IActivitiesRepository activitiesRepository) =>
            activitiesRepository.GetQueryable();

        public IQueryable<ActivityDto> GetActivities(IActivitiesRepository activitiesRepository) =>
            SelectActivities(activitiesRepository.GetQueryable());

        private IQueryable<ActivityDto> SelectActivities(IQueryable<Activity> query)
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
                    Image = a.User.Photos.FirstOrDefault(x => x.IsMain).Url
                }).ToHashSet(),
            });
        }
    }
}
