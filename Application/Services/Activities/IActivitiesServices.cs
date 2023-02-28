using Application.Core;
using Domain.Models;

namespace Application.Services.Activities
{
    public interface IActivitiesServices
    {
        Task<Result<Activity>> CreateActivityAsync(Activity activity);
        Task<Result<IEnumerable<Activity>>> DeleteActivityAsync(Guid id);
        Task<Result<Activity>> EditActivityAsync(Activity message);
        Task<Result<IEnumerable<Activity>>> GetActivitiesAsync();
        Task<Result<Activity>> GetActivityDetailsAsync(Guid id);
    }
}
