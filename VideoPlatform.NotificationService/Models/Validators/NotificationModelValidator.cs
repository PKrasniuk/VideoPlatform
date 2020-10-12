using FluentValidation;
using VideoPlatform.Common.Infrastructure.Constants;
using VideoPlatform.NotificationService.Models.RequestModels;

namespace VideoPlatform.NotificationService.Models.Validators
{
    internal class NotificationModelValidator : AbstractValidator<NotificationModel>
    {
        public NotificationModelValidator()
        {
            RuleFor(x => x.Key).NotNull().NotEmpty().MaximumLength(FieldConstants.BaseFieldLength);
            RuleFor(x => x.Message).NotNull().NotEmpty();
        }
    }
}