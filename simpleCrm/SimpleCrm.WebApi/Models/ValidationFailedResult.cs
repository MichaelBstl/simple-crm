using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SimpleCrm.WebApi.Models
{
    public class ValidationFailedResult : ObjectResult 
    {
        public ValidationFailedResult(ModelStateDictionary modelState)
            : base(new ValidationStateModel(modelState))
        {
            StatusCode = StatusCodes.Status422UnprocessableEntity;
        }
    }
}
