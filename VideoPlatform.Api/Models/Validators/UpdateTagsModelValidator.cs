using FluentValidation;
using VideoPlatform.Api.Models.RequestModels;
using VideoPlatform.BLL.Interfaces;

namespace VideoPlatform.Api.Models.Validators
{
    internal class UpdateTagsModelValidator : AbstractValidator<UpdateTagsModel>
    {
        public UpdateTagsModelValidator(ITagManager tagManager)
        {
            RuleForEach(x => x.Tags).NotNull().SetValidator(new UpdateTagModelValidator(tagManager));
        }
    }
}