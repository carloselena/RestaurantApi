using RestaurantApi.Core.Application.Exceptions;

namespace RestaurantApi.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await ExceptionHandler(httpContext, ex);
            }
        }

        private async Task ExceptionHandler(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";

            var statusCode = ex switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                ValidationErrorException => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError
            };

            context.Response.StatusCode = statusCode;

            object response = ex switch
            {
                NotFoundException => new
                {
                    type = "NotFound",
                    message = ex.Message,
                },

                ValidationErrorException ve => new
                {
                    type = "ValidationError",
                    errors = ve.Errors
                },

                _ => new
                {
                    type = "ServerError",
                    message = "Ocurrió un error inesperado."
                }
            };

            await context.Response.WriteAsJsonAsync(response);
        }
    }
}
