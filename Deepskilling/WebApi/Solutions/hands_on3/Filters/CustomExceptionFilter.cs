using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace hands_on3.Filters
{
    public class CustomExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            File.AppendAllText("exceptions.txt", context.Exception.ToString() + Environment.NewLine);
            context.Result = new ObjectResult("Internal server error occurred")
            {
                StatusCode = 500
            };
        }
    }
}