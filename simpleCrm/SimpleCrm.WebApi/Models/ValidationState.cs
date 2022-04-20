using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;

namespace SimpleCrm.WebApi.Models
{
    public class ValidationState
    {
        public List<ValidationError> Errors { get; set; }
    }
}
