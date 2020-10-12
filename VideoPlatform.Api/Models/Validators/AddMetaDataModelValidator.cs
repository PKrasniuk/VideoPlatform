﻿using FluentValidation;
using VideoPlatform.Api.Models.RequestModels;
using VideoPlatform.Common.Infrastructure.Constants;

namespace VideoPlatform.Api.Models.Validators
{
    internal class AddMetaDataModelValidator : AbstractValidator<AddMetaDataModel>
    {
        public AddMetaDataModelValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty().MaximumLength(FieldConstants.BaseFieldLength);
            RuleFor(x => x.Description).NotNull().NotEmpty().MaximumLength(FieldConstants.BigFieldLength);
            RuleFor(x => x.Value).NotNull().NotEmpty();
            RuleFor(x => x.Type).NotNull();
        }
    }
}