using Microsoft.AspNetCore.Builder;

namespace Security.Transversal.Logger
{
    public static class UseCustomLogger
    {
        public static void UseMiddlewareLogging(this IApplicationBuilder app) => app.UseMiddleware<LoggerExceptionMiddleware>();
    }
}
