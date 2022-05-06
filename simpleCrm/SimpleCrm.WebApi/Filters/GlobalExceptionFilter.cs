using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;

namespace SimpleCrm.WebApi.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter, IDisposable
    {
        private readonly ILogger<GlobalExceptionFilter> _logger;

        public GlobalExceptionFilter(
            ILogger<GlobalExceptionFilter> logger) 
        { 
            _logger = logger; 
        }
        public void Dispose() { }

        public void OnException(ExceptionContext context)
        {
            // Check for ApiException in context.Exception, 
            // - if present use its status code and model in the object result below
            // - if another type, pick a default status code and anything you prefer for the model

            // no return type, instead you set the context.Result as last line in this method

            var model = context.Exception is ApiException ? ((ApiException)context.Exception).Model
                : new { Message = "this is a test" };
            var statusCode = context.Exception is ApiException ? ((ApiException)context.Exception).StatusCode
                : 200;
            var message = context.Exception is ApiException ? ((ApiException)context.Exception).Message
                : "this is  test error message";

            var returnModel = new
            {
                success = false,
                message = new string[] { message },
                model = model
            };
            _logger.LogError("Status Code: {0} Details: {1}", new EventId(statusCode), context.Exception);

            context.Result = new ObjectResult(returnModel) { StatusCode = statusCode };
        }
    }
}