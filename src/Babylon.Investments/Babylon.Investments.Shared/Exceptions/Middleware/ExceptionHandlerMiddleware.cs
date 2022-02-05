using System;
using System.Net;
using System.Threading.Tasks;
using Babylon.Investments.Shared.Exceptions.Custom;
using Babylon.Investments.Shared.Exceptions.Model;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Babylon.Investments.Shared.Exceptions.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private const string JsonContentType = "application/json";
        private readonly RequestDelegate _request;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionHandlerMiddleware"/> class.
        /// </summary>
        /// <param name="next">The next.</param>
        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            this._request = next;
        }

        /// <summary>
        /// Invokes the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public Task Invoke(HttpContext context) => this.InvokeAsync(context);

        /// <summary>
        /// Invokes the specified context asynchronously.
        /// </summary>
        /// <param name="context">The Http Context.</param>
        /// <returns></returns>
        async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await this._request(context);
            }
            catch (Exception exception)
            {
                await HandleException(context, exception);
            }
        }

        /// <summary>
        /// Handles the exception to send back the error to the client.
        /// </summary>
        /// <param name="context">The Http Context.</param>
        /// <param name="exception">The exception thrown.</param>
        /// <returns></returns>
        private async Task HandleException(HttpContext context, Exception exception)
        {
            // set http status code and content type
            context.Response.StatusCode = GetStatusCodeFromException(exception);
            context.Response.ContentType = JsonContentType;

            // writes / returns error model to the response
            await context.Response.WriteAsync(
                JsonConvert.SerializeObject(new ErrorObjectResult
                {
                    Message = exception.Message
                }));

            context.Response.Headers.Clear();
        }

        /// <summary>
        /// Configures/maps exception to the proper HTTP error Type
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <returns></returns>
        private static int GetStatusCodeFromException(Exception exception)
        {
            var httpStatusCode = exception switch
            {
                var _ when exception is BabylonException => (int)HttpStatusCode.BadRequest,
                _ => (int)HttpStatusCode.InternalServerError,
            };

            return httpStatusCode;
        }
    }
}
