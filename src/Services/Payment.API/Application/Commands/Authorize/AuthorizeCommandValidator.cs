using FluentValidation;

namespace Payment.API.Application.Commands.Authorize
{
    public class AuthorizeCommandValidator : AbstractValidator<AuthorizeCommand>
    {
        public AuthorizeCommandValidator()
        {
            RuleFor(r => r.Amount).GreaterThan(0).NotNull().WithMessage("Please enter a valid amount.");
            RuleFor(r => r.CVV).NotNull().LessThan(1000).GreaterThan(0).WithMessage("Please enter a valid CVV number.");
            RuleFor(r => r.ExpirationMonth).NotNull().NotEmpty().LessThan(13).GreaterThan(0).WithMessage("Please enter a valid card expiration month");
            RuleFor(r => r.ExpirationYear).NotNull().NotEmpty().WithMessage("Please enter a valid card expiration year.");
            RuleFor(r => r.OrderReference).NotNull().NotEmpty().MaximumLength(50).WithMessage("Please enter a valid order reference number.");
            RuleFor(r => r.CardholderNumber).NotNull().Length(16).NotEmpty().WithMessage("Please enter a valid card number.");
            RuleFor(r => r.HolderName).NotNull().NotEmpty().WithMessage("Please enter a valid card holder name.");
            RuleFor(r => r.Currency).NotNull().NotEmpty().Length(3).WithMessage("Please enter a valid currency.");

        }
    }
}
