using ESP.Cloud.BE.Email.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace ESP.Cloud.BE.Email
{
    public static class DependencyInjection
    {
        public static void AddEmail(this IServiceCollection services)
        {
            services.AddScoped<IEmail, EmailService>();
        }
    }
}
