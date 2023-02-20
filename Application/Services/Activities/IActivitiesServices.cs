using Domain.Models;

namespace Application.Services.Activities
{
    public interface IActivitiesServices
    {
        Task<Activity> CreateActivityAsync(Activity activity);
        Task<IEnumerable<Activity>> DeleteActivityAsync(Guid id);
        Task<Activity> EditActivityAsync(Activity message);
        Task<IEnumerable<Activity>> GetActivitiesAsync();
        Task<Activity> GetActivityDetailsAsync(Guid id);
    }
}
