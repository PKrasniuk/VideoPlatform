using System.Linq;
using FluentValidation;
using VideoPlatform.Api.Models.RequestModels;
using VideoPlatform.BLL.Interfaces;
using VideoPlatform.Common.Infrastructure.Constants;
using VideoPlatform.Common.Infrastructure.Helpers;

namespace VideoPlatform.Api.Models.Validators
{
    internal class AddTagModelValidator : AbstractValidator<AddTagModel>
    {
        public AddTagModelValidator(ITagManager tagManager)
        {
            var tags = AsyncHelper.RunSync(async () => await tagManager.GetTagsAsync());

            RuleFor(x => x.Name).NotNull().NotEmpty().MaximumLength(FieldConstants.HalfFieldLength);
            RuleFor(x => x.Name).Must((_, name) => tags.All(p => p.Name != name)).WithMessage("Name must be unique");
        }
    }
}