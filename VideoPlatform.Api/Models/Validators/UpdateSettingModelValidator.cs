using System.Linq;
using FluentValidation;
using VideoPlatform.Api.Models.RequestModels;
using VideoPlatform.BLL.Interfaces;
using VideoPlatform.Common.Infrastructure.Constants;
using VideoPlatform.Common.Infrastructure.Helpers;

namespace VideoPlatform.Api.Models.Validators;

internal class UpdateSettingModelValidator : AbstractValidator<UpdateSettingModel>
{
    public UpdateSettingModelValidator(ISettingManager settingManager)
    {
        var settings = AsyncHelper.RunSync(async () => await settingManager.GetSettingsAsync());

        RuleFor(x => x.Id).NotNull().NotEmpty().GreaterThan((short)0);
        RuleFor(x => x.Name).NotNull().NotEmpty().MaximumLength(FieldConstants.BaseFieldLength);
        RuleFor(x => x.Name).Must((_, name) => settings.All(p => p.Name != name))
            .WithMessage("Name must be unique");
        RuleFor(x => x.Value).NotNull().NotEmpty();
    }
}