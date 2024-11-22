using AutoMapper;

namespace FishingAndCyclingApp.Extensions
{
    public static class AutoMapperExtensions
    {
        public static void ConfigureAutoMapper(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
               
                cfg.CreateMap<Models.User, DTOs.UserDto>().ReverseMap();
                cfg.CreateMap<Models.User, DTOs.CreateUserDto>().ReverseMap();
                cfg.CreateMap<Models.User, DTOs.UpdateUserDto>().ReverseMap();

                cfg.CreateMap<Models.Route, DTOs.RouteDto>().ReverseMap();
                cfg.CreateMap<Models.Route, DTOs.CreateRouteDto>().ReverseMap();
                cfg.CreateMap<Models.Route, DTOs.UpdateRouteDto>().ReverseMap();

                cfg.CreateMap<Models.FishingSpot, DTOs.FishingSpotDto>().ReverseMap();
                cfg.CreateMap<Models.FishingSpot, DTOs.CreateFishingSpotDto>().ReverseMap();
                cfg.CreateMap<Models.FishingSpot, DTOs.UpdateFishingSpotDto>().ReverseMap();
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
