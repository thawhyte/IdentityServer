using Microsoft.AspNetCore.Builder;
using System.Collections.Generic;
using TheCoreBanking.Authenticate.Middleware;

namespace TheCoreBanking.Authenticate.Extensions
{
    public static class IApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseSecurityHeaders(this IApplicationBuilder app, Dictionary<string, string> headers)
        {
            app.UseMiddleware<SecurityHeadersMiddleware>(headers);
            return app;
        }
    }
}
