using Application.Core;
using Application.Interfaces;
using Domain.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Reactivities.Application.DTOs.Comments;
using Reactivities.Application.Models.Comments;
using Reactivities.Application.Queries.Comments;
using Reactivities.Application.Validators.Comments;
using Reactivities.Domain.Models;

namespace Reactivities.Application.Services.Comments
{
    public class CommentServices : ICommentServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserAccessor _userAccessor;
        private readonly ICommentQueryBuilder _commentQueryBuilder;

        public CommentServices(IUnitOfWork unitOfWork,
                               IUserAccessor userAccessor,
                               ICommentQueryBuilder commentQueryBuilder)
        {
            _unitOfWork = unitOfWork;
            _userAccessor = userAccessor;
            _commentQueryBuilder = commentQueryBuilder;
        }

        public class CommandValidator : AbstractValidator<Comment>
        {
            public CommandValidator()
            {
                RuleFor(x => x).SetValidator(new CommentsValidators());
            }
        }

        public async Task<Result<IEnumerable<CommentDto>>> GetCommentsAsync(Guid activityId)
        {
            var comments = await _commentQueryBuilder.GetActivityComments(_unitOfWork.Comments,
                                                                          activityId)
                                                     .OrderByDescending(a => a.CreatedAt)
                                                     .ToListAsync();
            return Result<IEnumerable<CommentDto>>.Success(comments);
        }

        public async Task<Result<CommentDto>> GetComment(int id)
        {
            var comment = await _commentQueryBuilder.GetComments(_unitOfWork.Comments)
                                                    .FirstOrDefaultAsync(a => a.Id == id);

            if (comment == null) return null;

            return Result<CommentDto>.Success(comment);
        }

        public async Task<Result<CommentDto>> InsertCommentsAsync(CommentsValues comments)
        {
            var activity = await _unitOfWork.Activities.GetQueryable(a => a.Id == comments.ActivityId)
                                                       .FirstOrDefaultAsync();

            if (activity == null) return null;

            var user = await _unitOfWork.Users.GetQueryable()
                                              .SingleOrDefaultAsync(s => s.UserName == _userAccessor.GetUername());

            var comment = new Comment
            {
                Author = user,
                Activity = activity,
                Body = comments.Body
            };

            activity.Comments.Add(comment);

            var success = await _unitOfWork.SaveChangesAsync();

            return success
                ? await GetComment(comment.Id)
                : Result<CommentDto>.Failure("Failed to add comment");
        }
    }
}
