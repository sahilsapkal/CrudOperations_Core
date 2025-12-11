using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CustomMiddleWare.Middleware
{
    public class RequestLoggingController : Controller
    {
        private readonly RequestDelegate _logger;
        public RequestLoggingController(RequestDelegate logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var sw = Stopwatch.StartNew();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Request: {context.Request.Method} {context.Request.Path}");
            Console.ResetColor();
            await _logger(context);

            sw.Stop();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Request: {context.Response.StatusCode} in {sw.ElapsedMilliseconds} ms");
            Console.ResetColor();
        }
    }
}
