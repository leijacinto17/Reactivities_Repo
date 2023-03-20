using Application.Core;
using Application.DTOs.Activities;
using Domain.Models;
using Reactivities.Application.Core;

namespace Application.Services.Activities
{
    public interface IActivitiesServices
    {
        Task<Result<ActivityDto>> CreateActivityAsync(Activity activity);
        Task<Result<bool>> DeleteActivityAsync(Guid id);
        Task<Result<ActivityDto>> EditActivityAsync(Activity message);
        Task<Result<PageList<ActivityDto>>> GetActivitiesAsync(PagingParams pagingParams);
        Task<Result<ActivityDto>> GetActivityDetailsAsync(Guid id);
        Task<Result<ActivityDto>> UpdateAttendance(Guid id);
    }
}
