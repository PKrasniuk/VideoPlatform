using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using VideoPlatform.NotificationService.Models.RequestModels;
using VideoPlatform.NotificationService.Models.Validators;

namespace VideoPlatform.NotificationService.Infrastructure.Extensions
{
    /// <summary>
    /// ValidationExtension
    /// </summary>
    public static class ValidationExtension
    {
        /// <summary>
        /// AddValidatorsCollection
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static void AddValidatorsCollection(this IServiceCollection services)
        {
            services.AddTransient<IValidator<NotificationModel>, NotificationModelValidator>();
        }
    }
}