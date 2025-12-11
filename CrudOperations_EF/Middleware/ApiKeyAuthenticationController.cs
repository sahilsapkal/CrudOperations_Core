namespace CrudOperations_EF.Middleware
{
    public class ApiKeyAuthenticationController
    {
        private readonly RequestDelegate _next;
        private const string Apikey = "x-api-key";
        private const string ValidApikey = "Sahil";

        public ApiKeyAuthenticationController(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpcontext)
        {
            if (httpcontext.Request.Path.StartsWithSegments("/swagger"))
            {
                await _next(httpcontext);
                return;
            }
            if (!httpcontext.Request.Headers.TryGetValue(Apikey,out var ExtractedKey))
            {
                httpcontext.Response.StatusCode = 401;
                await httpcontext.Response.WriteAsync("Api Key Is Missing");
                return;

            }

            if(ExtractedKey != ValidApikey)
            {
                httpcontext.Response.StatusCode = 401;
                await httpcontext.Response.WriteAsync("Invalid Api Key Found");
                return;
            }

            await _next(httpcontext);
        }
    }
}
