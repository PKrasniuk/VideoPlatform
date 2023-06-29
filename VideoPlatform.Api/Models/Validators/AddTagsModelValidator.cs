using FluentValidation;
using VideoPlatform.Api.Models.RequestModels;
using VideoPlatform.BLL.Interfaces;

namespace VideoPlatform.Api.Models.Validators;

internal class AddTagsModelValidator : AbstractValidator<AddTagsModel>
{
    public AddTagsModelValidator(ITagManager tagManager)
    {
        RuleForEach(x => x.Tags).NotNull().SetValidator(new AddTagModelValidator(tagManager));
    }
}