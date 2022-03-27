using FluentValidation;

namespace Payment.API.Application.Commands.Void
{
    public class VoidCommandValidator : AbstractValidator<VoidCommand>
    {
        public VoidCommandValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty().WithMessage("Please enter a valid id.");
            RuleFor(r => r.OrderReference).NotEmpty().NotNull().MaximumLength(50).WithMessage("Please enter a valid order reference.");
        }
    }
}
