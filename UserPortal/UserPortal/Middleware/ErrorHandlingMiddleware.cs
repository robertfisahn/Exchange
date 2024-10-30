using Newtonsoft.Json;
using System.Net;
using UserPortal.Exceptions;

namespace UserPortal.Middleware
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (NotFoundException notFoundException)
            {
                await HandleExceptionAsync(context, HttpStatusCode.NotFound, notFoundException.Message);
            }
            catch (BadRequestException badRequestException)
            {
                await HandleExceptionAsync(context, HttpStatusCode.BadRequest, badRequestException.Message);
            } 
            catch (Exception)
            {
                await HandleExceptionAsync(context, HttpStatusCode.InternalServerError, "Something went wrong.");
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, HttpStatusCode statusCode, string message)
        {
            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json";
            var result = JsonConvert.SerializeObject(new { error = message });
            return context.Response.WriteAsync(result);
        }
    }
}