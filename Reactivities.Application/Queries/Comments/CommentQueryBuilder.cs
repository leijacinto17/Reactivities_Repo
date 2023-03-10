using Reactivities.Application.DTOs.Comments;
using Reactivities.Domain.Interfaces;
using Reactivities.Domain.Models;

namespace Reactivities.Application.Queries.Comments
{
    public class CommentQueryBuilder : ICommentQueryBuilder
    {
        public IQueryable<Comment> GetCommentEntity(ICommentRepository commentRepository) =>
            commentRepository.GetQueryable();

        public IQueryable<CommentDto> GetActivityComments(ICommentRepository commentRepository, Guid activityId) =>
            SelectComments(commentRepository.GetQueryable(a => a.Activity.Id == activityId));

        public IQueryable<CommentDto> GetComments(ICommentRepository commentRepository) =>
            SelectComments(commentRepository.GetQueryable());

        public IQueryable<CommentDto> SelectComments(IQueryable<Comment> query)
        {
            return query.Select(s => new CommentDto
            {
                Id = s.Id,
                CreatedAt = s.CreatedAt,
                Body = s.Body,
                Username = s.Author.UserName,
                DisplayName = s.Author.DisplayName,
                Image = s.Author.Photos.FirstOrDefault(a => a.IsMain).Url
            });
        }
    }
}
