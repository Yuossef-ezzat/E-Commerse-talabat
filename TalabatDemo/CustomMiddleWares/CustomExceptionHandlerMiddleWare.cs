using DomainLayer.Exceptions;
using Shared.ErrorModels;
using System.Text.Json;

namespace TalabatDemo.CustomMiddleWares
{
    public class CustomExceptionHandlerMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomExceptionHandlerMiddleWare> _logger;

        public CustomExceptionHandlerMiddleWare(RequestDelegate Next , ILogger<CustomExceptionHandlerMiddleWare> logger)
        {
            _next = Next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext Context)
        {
            
            try
            {
                await _next.Invoke(Context);
                await HandleNotFoundEndPointAsync(Context);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                await HandlerExceptionAsync(Context, ex);
            }

        }

        private static async Task HandlerExceptionAsync(HttpContext Context, Exception ex)
        {
            Context.Response.StatusCode = ex switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                _ => StatusCodes.Status500InternalServerError
            };
            var response = new ErrorToReturn
            {
                StutsCode = Context.Response.StatusCode,
                ErrorMsg = ex.Message
            };


            await Context.Response.WriteAsJsonAsync(response);
        }

        private static async Task HandleNotFoundEndPointAsync(HttpContext Context)
        {
            if (Context.Response.StatusCode == StatusCodes.Status404NotFound)
            {
                var response = new ErrorToReturn
                {
                    StutsCode = StatusCodes.Status404NotFound,
                    ErrorMsg = $"The Requested Uri '{Context.Request.Path}' is not found"
                };


                await Context.Response.WriteAsJsonAsync(response);

            }
        }
    }
}
