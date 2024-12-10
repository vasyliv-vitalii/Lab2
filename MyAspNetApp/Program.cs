using System.Reflection;
using System.Text;
using BLLayer.Authentication.Implementation;
using BLLayer.Authentication.Interfaces;
using BLLayer.Services;
using DALayer.CommandRepositories;
using DALayer.DataBase;
using DALayer.QueryRepositories;
using DomainLayer.Abstraction.ICommandRepositories;
using DomainLayer.Abstraction.IQueryRepositories;
using DomainLayer.Abstraction.IServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MyAspNetApp.Middlewares;

namespace MyAspNetApp
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
            builder.Services.AddScoped<IUserService, UserService>();

            
            builder.Services.AddScoped<IFishingSpotCommandRepository, FishingSpotCommandRepository>();
            builder.Services.AddScoped<IFishingSpotQueryRepository, FishingSpotQueryRepository>();
            builder.Services.AddScoped<IFishingSpotService, FishingSpotService>();

            
            builder.Services.AddScoped<IBikeRouteCommandRepository, BikeRouteCommandRepository>();
            builder.Services.AddScoped<IBikeRouteQueryRepository, BikeRouteQueryRepository>();
            builder.Services.AddScoped<IBikeRouteService, BikeRouteService>();
            
            builder.Services.AddScoped<IAuthService, AuthService>();
        
            builder.Services.AddMemoryCache();
            
            builder.Services.AddScoped<IdentityInfoProvider>();
            builder.Services.AddScoped<IIdentityInfoSetter>(provider => provider.GetService<IdentityInfoProvider>());
            builder.Services.AddScoped<IIdentityInfoGetter>(provider => provider.GetService<IdentityInfoProvider>());

            builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
   

            builder.Services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header, add Bearer before token.",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                });
                
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });
            
            var jwtSettings = config.GetSection("JwtSettings");
            var key = jwtSettings["SecretKey"];
            
            builder.Services.AddAuthentication()
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false,
                        ValidateIssuer = false,
                        ValidateLifetime = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
                    };
                });

            var app = builder.Build();

            // Configure the HTTP request pipeline
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
                    c.OAuthClientId("swagger-ui");
                    c.OAuthAppName("Swagger UI");
                });
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMiddleware<AccessTokenHandlingMiddleware>();
            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Fishing and Routes API V1");
                c.RoutePrefix = string.Empty;
            });

            app.MapControllers();

            app.Run();
        }
    }
}
