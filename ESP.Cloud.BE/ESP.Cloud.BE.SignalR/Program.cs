using ESP.Cloud.BE.Core.BaseDL;
using ESP.Cloud.BE.Core.DL;
using ESP.Cloud.BE.Infrastructure;
using ESP.Cloud.BE.Infrastructure.Repository;
using Microsoft.Extensions.Configuration;

namespace ESP.Cloud.BE.SignalR
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Handle Cors
            builder.Services.AddCors(p => p.AddPolicy("MyCors", build =>
            {
                build.WithOrigins("http://localhost:5173") // chỉ định nguồn cụ thể
               .AllowAnyMethod()
               .AllowAnyHeader()
               .AllowCredentials();
            }));

            // Add services to the container.
            builder.Services.AddAuthorization();
            builder.Services.AddSignalR();
            //var connectionString = builder.Configuration.GetConnectionString("ESP");
            //builder.Services.AddScoped<IUnitOfWork>((provider => new UnitOfWork(connectionString)));

            //builder.Services.AddScoped<IBookingDL, BookingDL>();
            //builder.Services.AddHostedService<ServerTimeNotifier>();
            //builder.Services.AddSingleton<Func<IBookingDL>>(provider => provider.GetRequiredService<IBookingDL>);

            var app = builder.Build();
            app.UseCors("MyCors");

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.MapHub<NotificationsHub>("/notifications");
            app.Run();
        }
    }
}