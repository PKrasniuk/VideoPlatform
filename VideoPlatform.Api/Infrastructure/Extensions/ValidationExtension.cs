using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using VideoPlatform.Api.Models.RequestModels;
using VideoPlatform.Api.Models.Validators;

namespace VideoPlatform.Api.Infrastructure.Extensions
{
    /// <summary>
    /// Validation Extension
    /// </summary>
    public static class ValidationExtension
    {
        /// <summary>
        /// Add Validators Collection
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static void AddValidatorsCollection(this IServiceCollection services)
        {
            services.AddTransient<IValidator<AddPartnerTypesModel>, AddPartnerTypesModelValidator>();
            services.AddTransient<IValidator<AddPartnerModel>, AddPartnerModelValidator>();
            services.AddTransient<IValidator<UpdatePartnerModel>, UpdatePartnerModelValidator>();
            services.AddTransient<IValidator<RemovePartnerTypesModel>, RemovePartnerTypesModelValidator>();
            services.AddTransient<IValidator<FilterPartnerModel>, FilterPartnerModelValidator>();
            services.AddTransient<IValidator<AddSettingModel>, AddSettingModelValidator>();
            services.AddTransient<IValidator<UpdateSettingModel>, UpdateSettingModelValidator>();
            services.AddTransient<IValidator<AddTagModel>, AddTagModelValidator>();
            services.AddTransient<IValidator<AddTagsModel>, AddTagsModelValidator>();
            services.AddTransient<IValidator<FilterTagModel>, FilterTagModelValidator>();
            services.AddTransient<IValidator<UpdateTagModel>, UpdateTagModelValidator>();
            services.AddTransient<IValidator<RemoveTagsModel>, RemoveTagsModelValidator>();
            services.AddTransient<IValidator<AddMetaDataModel>, AddMetaDataModelValidator>();
            services.AddTransient<IValidator<UpdateMetaDataModel>, UpdateMetaDataModelValidator>();
            services.AddTransient<IValidator<AddInfoDataModel>, AddInfoDataModelValidator>();
            services.AddTransient<IValidator<UpdateInfoDataModel>, UpdateInfoDataModelValidator>();
            services.AddTransient<IValidator<InputTripModel>, InputTripModelValidator>();
        }
    }
}