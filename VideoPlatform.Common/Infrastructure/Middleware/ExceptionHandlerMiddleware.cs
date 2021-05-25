using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Serilog;
using VideoPlatform.Common.Models.ResponseModels;

namespace VideoPlatform.Common.Infrastructure.Middleware
{
    /// <summary>
    /// Exception Handler Middleware
    /// </summary>
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IWebHostEnvironment _environment;
        private static readonly ILogger Logger = Log.ForContext<ExceptionHandlerMiddleware>();

        /// <summary>
        /// ExceptionHandlerMiddleware
        /// </summary>
        /// <param name="next"></param>
        /// <param name="environment"></param>
        public ExceptionHandlerMiddleware(RequestDelegate next, IWebHostEnvironment environment)
        {
            _next = next;
            _environment = environment;
        }

        /// <summary>
        /// Invoke
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                Logger.Error($"Something went wrong: {ex}");
                await HandleExceptionAsync(httpContext, ex, _environment);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception, IWebHostEnvironment environment)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;

            return context.Response.WriteAsync(new ErrorDetailsModel
            {
                Type = environment.IsDevelopment() ? exception.GetType().ToString() : string.Empty,
                Title = "Internal Server Error",
                Status = context.Response.StatusCode,
                Errors = environment.IsDevelopment()
                    ? exception.InnerException != null
                        ? exception.InnerException.Message
                        : exception.Message
                    : string.Empty,
                TraceId = Guid.NewGuid().ToString()
            }.ToString());
        }
    }
}