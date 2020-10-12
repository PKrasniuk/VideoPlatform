using FluentValidation;
using VideoPlatform.Api.Models.RequestModels;

namespace VideoPlatform.Api.Models.Validators
{
    internal class FilterPartnerModelValidator : AbstractValidator<FilterPartnerModel>
    {
        public FilterPartnerModelValidator()
        {
            RuleFor(x => x.PageNumber).NotNull().NotEmpty().GreaterThan(0);
            RuleFor(x => x.PageSize).NotNull().NotEmpty().GreaterThan(0);
            RuleFor(x => x.SortedProperty).NotNull();
            RuleFor(x => x.SortOrder).NotNull();
        }
    }
}