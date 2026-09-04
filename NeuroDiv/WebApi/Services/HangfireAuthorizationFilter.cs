using Hangfire.Dashboard;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WebApi.Services
{
    public class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            var httpContext = context.GetHttpContext();

            // Only allow SuperAdmin in production
            // In development, allow all
            if (httpContext.RequestServices.GetRequiredService<IWebHostEnvironment>().IsDevelopment())
                return true;

            return httpContext.User.Identity?.IsAuthenticated == true
                && httpContext.User.IsInRole("SuperAdmin");
        }
    }
}
