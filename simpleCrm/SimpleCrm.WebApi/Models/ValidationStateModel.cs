using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;

namespace SimpleCrm.WebApi.Models
{
    public class ValidationStateModel
    {
        public string Messages;
        public List<ValidationError> Errors;
        public ValidationStateModel(ModelStateDictionary modelState)
        {  // modelState has keys for each property that has an associated error
            var genericErrors = modelState.Keys
                .Where(key => string.IsNullOrWhiteSpace(key)) // model only errors have an empty key
                .Select(key => modelState[key].Errors.Select(x => x.ErrorMessage))
                .ToList();

            Messages = genericErrors.Count == 0 ? "Validation failed"
                : string.Join(".", genericErrors.Distinct());

            // now get the property level errors, they have a proeperty name as a key
            Errors = modelState.Keys
                .Where(key => !string.IsNullOrWhiteSpace(key))
                // some Linq magic ...
                .SelectMany(key => modelState[key].Errors
                    .Select(x => new ValidationError { Field = key, Message = x.ErrorMessage })).ToList();
        }
    }
}
