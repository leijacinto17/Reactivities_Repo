using Application.Core;
using Reactivities.Application.DTOs.Comments;
using Reactivities.Application.Models.Comments;

namespace Reactivities.Application.Services.Comments
{
    public interface ICommentServices
    {
        Task<Result<IEnumerable<CommentDto>>> GetCommentsAsync(Guid activityId);
        Task<Result<CommentDto>> InsertCommentsAsync(CommentsValues comments);
    }
}