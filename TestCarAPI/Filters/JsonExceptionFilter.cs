using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TestCarAPI.Models;

namespace TestCarAPI.Filters
{
    public class JsonExceptionFilter : IExceptionFilter
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        
        public JsonExceptionFilter(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public void OnException(ExceptionContext context)
        {
            var error = new ApiError();

            if (_webHostEnvironment.IsDevelopment())
            {
                error.Message = context.Exception.Message;
                error.Detail = context.Exception.StackTrace ?? string.Empty;
            }
            else
            {
                error.Message = @"A server error occurred";
                error.Detail = context.Exception.Message;
            }

            context.Result = new ObjectResult(error)
            {
                StatusCode = 500
            };
        }
    }
}
