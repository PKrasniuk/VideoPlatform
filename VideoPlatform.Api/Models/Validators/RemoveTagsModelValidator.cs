using FluentValidation;
using FluentValidation.Validators;
using VideoPlatform.Api.Models.RequestModels;

namespace VideoPlatform.Api.Models.Validators
{
    internal class RemoveTagsModelValidator : AbstractValidator<RemoveTagsModel>
    {
        public RemoveTagsModelValidator()
        {
            RuleForEach(x => x.Ids).NotNull().NotEmpty().SetValidator(new GreaterThanOrEqualValidator<RemoveTagsModel, int>(0));
        }
    }
}