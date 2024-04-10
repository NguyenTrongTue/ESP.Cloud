using ESP.Cloud.BE.Core.BaseDL;
using ESP.Cloud.BE.Core.DL;
using ESP.Cloud.BE.Core.DL.GarageDL;
using ESP.Cloud.BE.Core.DL.UserDL;
using ESP.Cloud.BE.Infrastructure.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace ESP.Cloud.BE.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuation)
        {
            services.AddScoped<IGarageDL, GarageDL>();
            services.AddScoped<IBookingDL, BookingDL>();
            services.AddScoped<ICarDL, CarDL>();
            services.AddScoped<IUserDL, UserDL>();
            services.AddScoped<INotificationDL, NotificationDL>();
            services.AddScoped<IQuestionsDL, QuestionsDL>();
            services.AddScoped<IAnswerDL, AnswerDL>();

            var connectionString = configuation.GetConnectionString("ESP");
            services.AddScoped<IUnitOfWork>((provider => new UnitOfWork(connectionString)));
        }

    }
}
