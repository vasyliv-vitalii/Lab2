using FishingAndCyclingApp.Data;
using FishingAndCyclingApp.Extensions;
using FishingAndCyclingApp.Models;
using FishingAndCyclingApp.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace FishingAndCyclingApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

            // Add DbContext for SQLite, make sure "DefaultConnection" is correctly set in your appsettings.json
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Add repository services (make sure these interfaces and implementations are defined in your project)
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IRouteRepository, RouteRepository>();
            builder.Services.AddScoped<IFishingSpotRepository, FishingSpotRepository>();

            // Add AutoMapper for mapping DTOs and entities (if needed)

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

            // Enable Swagger and Swagger UI
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Fishing and Routes API V1");
                c.RoutePrefix = string.Empty; // Set Swagger UI at the app's root
            });

            // Map controllers (this automatically maps the API controllers to routes)
            app.MapControllers();

            // Run the application
            app.Run();
        }
    }
}
