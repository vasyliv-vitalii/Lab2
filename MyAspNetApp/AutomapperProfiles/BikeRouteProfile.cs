using AutoMapper;
using DomainLayer.Models;
using MyAspNetApp.DTOs;

namespace MyAspNetApp.AutomapperProfiles
{
    public class BikeRouteProfile : Profile
    {
        public BikeRouteProfile()
        {
            CreateMap<BikeRoute, BikeRouteDTO>();
            CreateMap<CreateUpdateBikeRouteDto, BikeRoute>();
        }
    }
}
