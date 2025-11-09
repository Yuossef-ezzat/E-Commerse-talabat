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
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                //set status code
                Context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                //set content type
                //Context.Response.ContentType = "application/json";
                //create response object
                var response = new ErrorToReturn
                {
                    StutsCode = Context.Response.StatusCode,
                    ErrorMsg = ex.Message
                };
                //serialize response object
                //var responseString = JsonSerializer.Serialize(response);
                //return response
                await Context.Response.WriteAsJsonAsync(response);
            }

        }
    }
}
