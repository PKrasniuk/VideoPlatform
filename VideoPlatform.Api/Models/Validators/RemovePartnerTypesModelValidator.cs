using FluentValidation;
using VideoPlatform.Api.Models.RequestModels;

namespace VideoPlatform.Api.Models.Validators;

internal class RemovePartnerTypesModelValidator : AbstractValidator<RemovePartnerTypesModel>
{
    public RemovePartnerTypesModelValidator()
    {
        RuleFor(x => x.PartnerId).NotNull().NotEmpty().GreaterThan(0);
        RuleFor(x => x.Type).NotNull();
    }
}