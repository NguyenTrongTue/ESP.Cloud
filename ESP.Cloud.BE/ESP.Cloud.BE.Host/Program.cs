using ESP.Cloud.BE.Cache;
using ESP.Cloud.BE.Core.ESPException;
using ESP.Cloud.BE.Core.Resource;
using ESP.Cloud.BE.Host.Middleware;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ESP.Cloud.BE.Jobs;
using ESP.Cloud.BE.Infrastructure;
using ESP.Cloud.BE.Application;
using ESP.Cloud.BE.Email;

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
            builder.Services.AddApplication();
            builder.Services.AddJobs();
            builder.Services.AddCache(builder.Configuration);
            builder.Services.AddInfrastructure(builder.Configuration);
            builder.Services.AddEmail();
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