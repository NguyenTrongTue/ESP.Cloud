using AutoMapper;
using ESP.Cloud.BE.Application.Interface;
using ESP.Cloud.BE.Application.Service;
using ESP.Cloud.BE.Cache;
using ESP.Cloud.BE.Core.BaseDL;
using ESP.Cloud.BE.Core.DL;
using ESP.Cloud.BE.Core.DL.GarageDL;
using ESP.Cloud.BE.Core.DL.UserDL;
using ESP.Cloud.BE.Core.ESPException;
using ESP.Cloud.BE.Core.Resource;
using ESP.Cloud.BE.Host.Middleware;
using ESP.Cloud.BE.Infrastructure.Repository;
using ESP.Cloud.BE.Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Reflection;

namespace ESP.Cloud.BE.Host
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            // Handle Cors
            builder.Services.AddCors(p => p.AddPolicy("MyCors", build =>
            {
                build.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
            }));

            // Add services to the container.
            builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState.Values
                    .SelectMany(x => x.Errors);

                    return new BadRequestObjectResult(JsonConvert.SerializeObject(new BaseException()
                    {

                        ErrorCode = 2,
                        UserMessage = Resource.ExceptionValidateDefault,
                        DevMessage = Resource.ExceptionValidateDefault,
                        TraceId = "",
                        MoreInfor = "",
                        Errors = errors
                    })); ;
                };
            }).AddJsonOptions(options =>
            {

                options.JsonSerializerOptions.PropertyNamingPolicy = null;
            });
            //builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddScoped<IRedisCache, RedisCache>();
            builder.Services.AddScoped<IGarageDL, GarageDL>();
            builder.Services.AddScoped<IGarageService, GarageService>();
            builder.Services.AddScoped<IBookingDL, BookingDL>();
            builder.Services.AddScoped<IBookingService, BookingService>();
            builder.Services.AddScoped<ICarService, CarService>();
            builder.Services.AddScoped<ICarDL, CarDL>();
            builder.Services.AddScoped<IAuthService, AuthSerivice>();
            builder.Services.AddScoped<IUserDL, UserDL>();
            builder.Services.AddScoped<IMapper, Mapper>();
            var connectionString = builder.Configuration.GetConnectionString("ESP");
            builder.Services.AddScoped<IUnitOfWork>((provider => new UnitOfWork(connectionString)));
            builder.Services.AddStackExchangeRedisCache(
                options =>
                {
                    options.Configuration = builder.Configuration.GetConnectionString("Redis");
                    options.InstanceName = "CacheESP_";
                });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStaticFiles();
            app.UseCors("MyCors");
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.UseMiddleware<ExceptionMiddleware>();
            app.MapControllers();

            app.Run();
        }
    }
}