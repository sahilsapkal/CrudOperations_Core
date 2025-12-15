using Microsoft.AspNetCore.Mvc.Filters;

namespace CrudOperations_EF.Filters
{
    public class ActionLoggingFilter : IActionFilter
    {
        public void OnActionExecuting( ActionExecutingContext context)
        {
            Console.WriteLine($"[FILTER LOG] Starting action: {context.ActionDescriptor.DisplayName}");

        }

        public void OnActionExecuted( ActionExecutedContext context )
        {
            Console.WriteLine($"[FILTER LOG] finished action: {context.ActionDescriptor.DisplayName}");

        }

    }
}
