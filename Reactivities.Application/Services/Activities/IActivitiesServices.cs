using Application.Core;
using Application.DTOs.Activities;
using Domain.Models;

namespace Application.Services.Activities
{
    public interface IActivitiesServices
    {
        Task<Result<ActivityDto>> CreateActivityAsync(Activity activity);
        Task<Result<IEnumerable<ActivityDto>>> DeleteActivityAsync(Guid id);
        Task<Result<ActivityDto>> EditActivityAsync(Activity message);
        Task<Result<IEnumerable<ActivityDto>>> GetActivitiesAsync();
        Task<Result<ActivityDto>> GetActivityDetailsAsync(Guid id);
        Task<Result<ActivityDto>> UpdateAttendance(Guid id);
    }
}
