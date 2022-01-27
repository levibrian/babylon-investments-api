using System;
using Ivas.Transactions.Api.Constants;
using Ivas.Transactions.Shared.Exceptions.Custom;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Ivas.Transactions.Api.Filters
{
    /// <summary>
    /// Specifies that the class or method that this attribute is applied to requires the specified authorization.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class IvasAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var request = context.HttpContext.Request;

            var loggerFactory = context.HttpContext.RequestServices.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger(nameof(IvasAuthorizeAttribute));
            
            var isRapidApiUserProvided = request.Headers.TryGetValue(IvasApiHeaders.RapidApiUserKey, out var rapidApiUser);
            var isRapidApiKeyProvided = request.Headers.TryGetValue(IvasApiHeaders.RapidApiKey, out var rapidApiKey);
            var isBabylonApiKeyProvided = request.Headers.TryGetValue(IvasApiHeaders.OverrideApiKey, out var overrideApiKey);

            logger.LogInformation($"Headers Raw - {string.Join(",", request.Headers.Values)}");
            logger.LogInformation($"Headers - Rapid Api User: {rapidApiUser}");
            logger.LogInformation($"Headers - Rapid Api Key: {rapidApiKey}");
            logger.LogInformation($"Headers - Babylon Api Key: {overrideApiKey}");
            
            if (isRapidApiUserProvided &&
                isRapidApiKeyProvided ||
                isBabylonApiKeyProvided) return;

            logger.LogInformation($"No headers provided. Forcing authentication error.");

            var response = context.HttpContext.Response;

            response.Headers.Add("AuthStatus", IvasApiHeaders.UnAuthorizedHeader);
            response.Headers.Add("AuthMessage", IvasApiHeaders.UnAuthorizedErrorMessage);

            context.Result = new JsonResult(IvasApiHeaders.UnAuthorizedHeader)
            {
                StatusCode = 401,
                Value = new
                {
                    Status = IvasApiHeaders.UnAuthorizedHeader,
                    Message = IvasApiHeaders.UnAuthorizedErrorMessage
                }
            };
        }
    }
}