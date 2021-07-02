using FluentValidation;

namespace ArrearsApi.V1.Domain
{
    public class AssetAddressValidator : AbstractValidator<AssetAddress>
    {
        public AssetAddressValidator()
        {
            RuleFor(x => x.AddressLine1).NotNull().NotEmpty();
            RuleFor(x => x.AddressLine2).NotNull().NotEmpty();
            RuleFor(x => x.AddressLine3).NotNull().NotEmpty();
            RuleFor(x => x.AddressLine4).NotNull().NotEmpty();
            RuleFor(x => x.PostCode).NotNull().NotEmpty();
        }
    }
}
