using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using DALayer.CommandRepositories;
using DALayer.DataBase;
using DALayer.QueryRepositories;
using DomainLayer.Abstarction.ICommandRepositories;
using DomainLayer.Abstarction.IQueryRepositories;

namespace FishingAndCyclingApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            ConfigurationManager config = builder.Configuration;

            // Add services to the container.
            builder.Services.AddControllers();

            // Add DbContext for SQLite, make sure "DefaultConnection" is correctly set in your appsettings.json
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseNpgsql(config.GetConnectionString("Default"));
                options.EnableSensitiveDataLogging();
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

            // Add repository services (make sure these interfaces and implementations are defined in your project)
            builder.Services.AddScoped<IUserCommandRepository, UserCommandRepository>();
            builder.Services.AddScoped<IUserQueryRepository, UserQueryRepository>();

            builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
   

            // Add Swagger for API documentation
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Fishing and Routes API", Version = "v1" });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Fishing and Routes API V1");
                c.RoutePrefix = string.Empty; // Set Swagger UI at the app's root
            });

            app.MapControllers();

            app.Run();
        }
    }
}
