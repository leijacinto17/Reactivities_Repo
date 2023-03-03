using Domain.Models;
using FluentValidation;

namespace Application.Validators.Activities
{
    public class ActivityValidators : AbstractValidator<Activity>
    {
        public ActivityValidators()
        {
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.Date).NotEmpty();
            RuleFor(x => x.Category).NotEmpty();
            RuleFor(x => x.City).NotEmpty();
            RuleFor(x => x.Venue).NotEmpty();
        }
    }
}
