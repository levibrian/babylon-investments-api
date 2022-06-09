using System;
using Babylon.Investments.Api.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Babylon.Investments.Api.Filters
{
    /// <summary>
    /// Specifies that the class or method that this attribute is applied to requires the specified authorization.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class BabylonAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var request = context.HttpContext.Request;

            var loggerFactory = context.HttpContext.RequestServices.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger(nameof(BabylonAuthorizeAttribute));
            
            var isRapidApiUserProvided = request.Headers.TryGetValue(BabylonApiHeaders.RapidApiUserKey, out var rapidApiUser);
            var isRapidApiKeyProvided = request.Headers.TryGetValue(BabylonApiHeaders.RapidApiKey, out var rapidApiKey);
            var isBabylonApiKeyProvided = request.Headers.TryGetValue(BabylonApiHeaders.OverrideApiKey, out var overrideApiKey);

            logger.LogInformation($"Headers Raw Keys - {string.Join(",", request.Headers.Keys)}");
            logger.LogInformation($"Headers Raw Values - {string.Join(",", request.Headers.Values)}");
            logger.LogInformation($"Headers - Rapid Api User: {rapidApiUser}");
            logger.LogInformation($"Headers - Rapid Api Key: {rapidApiKey}");
            logger.LogInformation($"Headers - Babylon Api Key: {overrideApiKey}");
            
            if (isRapidApiUserProvided &&
                isRapidApiKeyProvided ||
                isBabylonApiKeyProvided) return;

            logger.LogInformation($"No headers provided. Forcing authentication error.");

            var response = context.HttpContext.Response;

            response.Headers.Add("AuthStatus", BabylonApiHeaders.UnAuthorizedHeader);
            response.Headers.Add("AuthMessage", BabylonApiHeaders.UnAuthorizedErrorMessage);

            context.Result = new JsonResult(BabylonApiHeaders.UnAuthorizedHeader)
            {
                StatusCode = 401,
                Value = new
                {
                    Status = BabylonApiHeaders.UnAuthorizedHeader,
                    Message = BabylonApiHeaders.UnAuthorizedErrorMessage
                }
            };
        }
    }
}