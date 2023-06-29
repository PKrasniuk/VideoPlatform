using FluentValidation;
using VideoPlatform.Api.Models.RequestModels;

namespace VideoPlatform.Api.Models.Validators;

internal class InputTripModelValidator : AbstractValidator<InputTripModel>
{
    public InputTripModelValidator()
    {
        RuleFor(x => x.VendorId).NotNull().NotEmpty();
        RuleFor(x => x.RateCode).NotNull().NotEmpty();
        RuleFor(x => x.PassengerCount).NotNull().GreaterThan(0.0f);
        RuleFor(x => x.TripTime).NotNull().GreaterThan(0.0f);
        RuleFor(x => x.TripDistance).NotNull().GreaterThan(0.0f);
        RuleFor(x => x.PaymentType).NotNull().NotEmpty();
        RuleFor(x => x.FareAmount).NotNull();
    }
}