using FluentValidation;
using VideoPlatform.Api.Models.RequestModels;
using VideoPlatform.BLL.Interfaces;
using VideoPlatform.Common.Infrastructure.Constants;
using VideoPlatform.Common.Infrastructure.Helpers;
using VideoPlatform.Domain.Entities;

namespace VideoPlatform.Api.Models.Validators
{
    internal class AddSettingModelValidator : AbstractValidator<AddSettingModel>
    {
        public AddSettingModelValidator(ISettingManager settingManager)
        {
            var settings = AsyncHelper.RunSync(async () => await settingManager.GetSettingsAsync());

            RuleFor(x => x.Name).NotNull().NotEmpty().MaximumLength(FieldConstants.BaseFieldLength);
            RuleFor(x => x.Name).SetValidator(new UniqueValidator<Setting>(settings)).WithMessage("Name must be unique");
            RuleFor(x => x.Value).NotNull().NotEmpty();
        }
    }
}