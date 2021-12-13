using System;
using Ivas.Transactions.Api.Constants;
using Ivas.Transactions.Shared.Exceptions.Custom;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Ivas.Transactions.Api.Filters
{
    /// <summary>
    /// Specifies that the class or method that this attribute is applied to requires the specified authorization.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class IvasAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public IvasAuthorizeAttribute()
        {
        }
        
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var request = context.HttpContext.Request;

            if (request.Headers.TryGetValue(IvasApiHeaders.RapidApiUserKey, out var rapidApiUser) &&
                request.Headers.TryGetValue(IvasApiHeaders.RapidApiKey, out var rapidApiKey)) 
                return;
            
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