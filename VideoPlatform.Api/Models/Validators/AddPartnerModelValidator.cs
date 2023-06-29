using System.Linq;
using FluentValidation;
using VideoPlatform.Api.Models.RequestModels;
using VideoPlatform.BLL.Interfaces;
using VideoPlatform.Common.Infrastructure.Constants;
using VideoPlatform.Common.Infrastructure.Helpers;

namespace VideoPlatform.Api.Models.Validators;

internal class AddPartnerModelValidator : AbstractValidator<AddPartnerModel>
{
    public AddPartnerModelValidator(IPartnerManager partnerManager)
    {
        var partners = AsyncHelper.RunSync(async () => await partnerManager.GetPartnersAsync());

        RuleFor(x => x.Name).NotNull().NotEmpty().MaximumLength(FieldConstants.QuarterFieldLength);
        RuleFor(x => x.Name).Must((_, name) => partners.All(p => p.Name != name))
            .WithMessage("Name must be unique");
        RuleFor(x => x.Description).NotNull().NotEmpty().MaximumLength(FieldConstants.HalfFieldLength);
        RuleFor(x => x.ShowOnPartnerPage).NotNull();
        RuleFor(x => x.IsArchived).NotNull();
    }
}