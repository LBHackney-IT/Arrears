using FluentValidation;
using System;

namespace ArrearsApi.V1.Domain
{
    public class ArrearsValidator : AbstractValidator<Arrears>
    {
        public ArrearsValidator()
        {
            RuleFor(x => x.TargetId).Must(ValidateGuid).WithErrorCode("Not a guid");
            RuleFor(x => x.TotalCharged).GreaterThan(0);
            RuleFor(x => x.TotalPaid).NotEmpty().GreaterThanOrEqualTo(0);
            RuleFor(x => x.CurrentBalance).NotEmpty().GreaterThanOrEqualTo(0);
            RuleFor(x => x.TargetType).IsInEnum().WithMessage("TargetType is not a valid enum value");
            RuleFor(x => x.CreatedAt).NotEmpty()
                    .Must(date => date != default(DateTime))
                    .WithMessage("CreatedAt date is required");
            RuleFor(x => x.AssetAddress).SetValidator(new AssetAddressValidator());
            RuleFor(x => x.Person).SetValidator(new PersonValidator());

        }
        private bool ValidateGuid(Guid bar)
        {
            return Guid.TryParse(bar.ToString(), out var result);
        }
    }
    
}

