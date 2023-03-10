using Reactivities.Application.DTOs.Comments;
using Reactivities.Domain.Interfaces;
using Reactivities.Domain.Models;

namespace Reactivities.Application.Queries.Comments
{
    public interface ICommentQueryBuilder
    {
        IQueryable<CommentDto> GetActivityComments(ICommentRepository commentRepository, Guid activityId);
        IQueryable<Comment> GetCommentEntity(ICommentRepository commentRepository);
        IQueryable<CommentDto> GetComments(ICommentRepository commentRepository);
    }
}