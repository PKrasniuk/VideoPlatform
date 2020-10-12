using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using VideoPlatform.Common.Infrastructure.Constants;
using VideoPlatform.ExternalService.Models;

namespace VideoPlatform.ExternalService.Infrastructure.Extensions
{
    public static class ExternalExtension
    {
        public static IServiceCollection AddExternalServicesCollection(this IServiceCollection services, IConfiguration configuration)
        {
            IAsyncPolicy<HttpResponseMessage> httpWaitAndRetryPolicy = Policy
                .HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode).WaitAndRetryAsync(
                    ExternalServiceConstants.RetryCount, retryAttempt => TimeSpan.FromSeconds(retryAttempt),
                    (result, span, retryCount, ctx) => Console.WriteLine($"Retrying({retryCount})..."));

            IAsyncPolicy<HttpResponseMessage> fallbackPolicy = Policy
                .HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
                .FallbackAsync(FallbackAction, OnFallbackAsync);

            if (configuration.GetSection("ExternalServices") != null)
            {
                var externalServicesConfigCollection = configuration.GetSection("ExternalServices").Get<ICollection<ExternalServiceInfo>>();
                if (externalServicesConfigCollection != null && externalServicesConfigCollection.Any())
                {
                    foreach (var externalServicesConfig in externalServicesConfigCollection)
                    {
                        services.AddHttpClient(externalServicesConfig.Name, client =>
                        {
                            client.BaseAddress = new Uri(externalServicesConfig.Url);
                            if (externalServicesConfig.DefaultRequestHeaders != null &&
                                externalServicesConfig.DefaultRequestHeaders.Any())
                            {
                                foreach (var defaultRequestHeader in externalServicesConfig.DefaultRequestHeaders)
                                {
                                    client.DefaultRequestHeaders.Add(defaultRequestHeader.Name,
                                        defaultRequestHeader.Value);
                                }
                            }
                        }).AddPolicyHandler(Policy.WrapAsync(fallbackPolicy, httpWaitAndRetryPolicy));
                    }
                }
            }

            return services;
        }

        private static Task OnFallbackAsync(DelegateResult<HttpResponseMessage> response, Context context)
        {
            return Task.CompletedTask;
        }

        private static Task<HttpResponseMessage> FallbackAction(
            DelegateResult<HttpResponseMessage> responseToFailedRequest, Context context,
            CancellationToken cancellationToken)
        {
            return Task.FromResult(new HttpResponseMessage(responseToFailedRequest.Result.StatusCode)
            {
                Content = new StringContent(
                    $"The fallback executed, the original error was {responseToFailedRequest.Result.ReasonPhrase}")
            });
        }
    }
}