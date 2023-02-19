using Domain.Models;

namespace Application.Services.Activities
{
    public interface IActivitiesServices
    {
        Task<IEnumerable<Activity>> GetActivitiesAsync();
    }
}
