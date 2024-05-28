using AmdarisProject.Contracts;
using System.Net;

namespace AmdarisProject.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;   
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                //TODO: Add problem Details & Custom exceptions
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                var error = new Error
                {
                    StatusCode = context.Response.StatusCode.ToString(),
                    Message = e.Message
                };

                await context.Response.WriteAsync(error.ToString());
            }
        }
    }
}
