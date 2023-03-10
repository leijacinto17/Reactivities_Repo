using FluentValidation;
using Reactivities.Domain.Models;

namespace Reactivities.Application.Validators.Comments
{
    public class CommentsValidators : AbstractValidator<Comment>
    {
            public CommentsValidators()
            {
                RuleFor(x => x.Body).NotEmpty();
            }
    }
}
