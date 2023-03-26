using Application.Core;
using Application.DTOs.Activities;
using Domain.Models;
using Reactivities.Application.Core;
using Reactivities.Application.DTOs.Accounts;
using Reactivities.Application.Models.Activities;

namespace Application.Services.Activities
{
    public interface IActivitiesServices
    {
        Task<Result<ActivityDto>> CreateActivityAsync(Activity activity);
        Task<Result<bool>> DeleteActivityAsync(Guid id);
        Task<Result<ActivityDto>> EditActivityAsync(Activity message);
        Task<Result<PageList<ActivityDto>>> GetActivitiesAsync(ActivityParams pagingParams);
        Task<Result<ActivityDto>> GetActivityDetailsAsync(Guid id);
        Task<Result<IEnumerable<UserActivityDto>>> GetUserActivityAsync(string username, string predicate);
        Task<Result<ActivityDto>> UpdateAttendance(Guid id);
    }
}
