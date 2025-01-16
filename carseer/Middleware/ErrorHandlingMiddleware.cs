using migration_postgress.Response;

namespace carseer.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;

        public ErrorHandlingMiddleware(RequestDelegate next) {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);

                // Handle 404 responses
                if (context.Response.StatusCode == 404)
                {
                    context.Response.ContentType = "application/json";
                    var response = new APIResponse<string>(false, "Resource not found", null, 404);
                    await context.Response.WriteAsJsonAsync(response);
                }
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }


        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 500;

            var response = new APIResponse<string>(false, exception.Message, null, 500);
            return context.Response.WriteAsJsonAsync(response);
        }
    }
}
