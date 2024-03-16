using AutoMapper;
using ESP.Cloud.BE.Application.Interface;
using ESP.Cloud.BE.Application.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ESP.Cloud.BE.Application
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<IGarageService, GarageService>();
            services.AddScoped<IBookingService, BookingService>();
            services.AddScoped<ICarService, CarService>();
            services.AddScoped<IAuthService, AuthSerivice>();
            services.AddScoped<IMapper, Mapper>();
        }
    }
}
