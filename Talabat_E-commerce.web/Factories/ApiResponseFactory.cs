using Microsoft.AspNetCore.Mvc;
using Shared.ErrorModels;

namespace Talabat_E_commerce.web.Factories
{
    public static class ApiResponseFactory
    {
        public static IActionResult GenerateApiValidationErrorsResponse(ActionContext Context)
        {
            var Errors = Context.ModelState.Where(M => M.Value.Errors.Any())
            .Select(M => new ValidationError
            {
                Field = M.Key,
                Errors = M.Value.Errors.Select(E => E.ErrorMessage)
            });
            var Respons = new ValidationErrorToReturn()
            {
                ValidationErrors = Errors
            };
            return new BadRequestObjectResult(Respons);
        }

    }
}
