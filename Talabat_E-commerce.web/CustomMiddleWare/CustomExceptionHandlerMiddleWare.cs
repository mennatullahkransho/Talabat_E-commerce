using Azure;
using DomainLayer.Exceptions;
using Shared.ErrorModels;

namespace Talabat_E_commerce.web.CustomMiddleWare
{
    public class CustomExceptionHandlerMiddleWare
    {
        private readonly RequestDelegate requestDelegate;
        private readonly ILogger<CustomExceptionHandlerMiddleWare> logger;

        public CustomExceptionHandlerMiddleWare(RequestDelegate requestDelegate , ILogger<CustomExceptionHandlerMiddleWare> logger)
        {
            this.requestDelegate = requestDelegate;
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await requestDelegate.Invoke(httpContext);
                await HandleNotFoundEndPointAsync (httpContext);
            }
            catch (Exception ex)
            {

                logger.LogError(ex, "Somthing went wrong");

                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            var Response = new ErrorToReturn()
            {
                ErrorMessage = ex.Message
            };
            
            Response.StatusCode = ex switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                UnauthorizedException => StatusCodes.Status401Unauthorized,
                BadRequestException badRequestException => GetBadRequestErrors(badRequestException, Response),
                _ => StatusCodes.Status500InternalServerError
            };
            httpContext.Response.StatusCode = Response.StatusCode;
            await httpContext.Response.WriteAsJsonAsync(Response);
        }

        private static int GetBadRequestErrors(BadRequestException badRequestException, ErrorToReturn? response)
        {
            response.Errors= badRequestException.Errors;
            return StatusCodes.Status400BadRequest;
        }

        private static async Task HandleNotFoundEndPointAsync(HttpContext httpContext)
        {
            if (httpContext.Response.StatusCode == StatusCodes.Status404NotFound)
            {
                var Response = new ErrorToReturn()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    ErrorMessage = $"End Point {httpContext.Request.Path} is Not Found"
                };
                await httpContext.Response.WriteAsJsonAsync(Response);
            }
        }
    }
}
