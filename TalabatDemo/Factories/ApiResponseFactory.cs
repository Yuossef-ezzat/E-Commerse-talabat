using Microsoft.AspNetCore.Mvc;
using Shared.ErrorModels;

namespace TalabatDemo.Factories
{
    public static class ApiResponseFactory
    {
        public static IActionResult AddCustomApiResponceFactory(ActionContext Context)
        {
            
                
             var errors = Context.ModelState.Where(e => e.Value.Errors.Any())
                                            .Select(m => new ValidationErrors
                                            {
                                                Field = m.Key,
                                                ErrorMsg = m.Value.Errors.Select(er => er.ErrorMessage)
                                            });
             var errorToReturn = new ValidationErrorToReturn
             {
                 Errors = errors
             };

             return new BadRequestObjectResult(errorToReturn);
        }
    }
}
