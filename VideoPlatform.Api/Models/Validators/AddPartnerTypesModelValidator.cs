using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using VideoPlatform.Api.Models.RequestModels;
using VideoPlatform.BLL.Interfaces;
using VideoPlatform.Common.Infrastructure.Helpers;
using VideoPlatform.Domain.Entities;

namespace VideoPlatform.Api.Models.Validators;

internal class AddPartnerTypesModelValidator : AbstractValidator<AddPartnerTypesModel>
{
    private readonly IEnumerable<PartnerTypes> _partnerTypes;

    public AddPartnerTypesModelValidator(IPartnerTypesManager partnerTypesManager)
    {
        _partnerTypes = AsyncHelper.RunSync(async () => await partnerTypesManager.GetPartnerTypesCollectionAsync());

        RuleFor(x => x.PartnerId).NotNull().NotEmpty().GreaterThan(0);
        RuleFor(x => x.Type).NotNull();
        RuleFor(x => x).Must(IsNameUnique).WithMessage("New partner type must be unique");
    }

    private bool IsNameUnique(AddPartnerTypesModel newValue)
    {
        return _partnerTypes.All(item => item.PartnerId != newValue.PartnerId && item.Type != newValue.Type);
    }
}