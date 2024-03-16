using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ESP.Cloud.BE.Cache
{
    public static class DependencyInjection
    {
        public static void AddCache(this IServiceCollection serviceDescriptors, IConfiguration configuration)
        {
            serviceDescriptors.AddScoped<IRedisCache, RedisCache>();

            serviceDescriptors.AddStackExchangeRedisCache(
                options =>
                {
                    options.Configuration = configuration.GetConnectionString("Redis");
                    options.InstanceName = "CacheESP_";
                });
        }

    }
}
