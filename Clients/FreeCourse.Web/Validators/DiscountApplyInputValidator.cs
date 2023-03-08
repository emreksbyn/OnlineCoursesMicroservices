using FluentValidation;
using FreeCourse.Web.Models.DiscountServiceModels;

namespace FreeCourse.Web.Validators
{
    public class DiscountApplyInputValidator : AbstractValidator<DiscountApplyInput>
    {
        public DiscountApplyInputValidator()
        {
            RuleFor(d => d.Code).NotEmpty().WithMessage("Code is required.");
        }
    }
}