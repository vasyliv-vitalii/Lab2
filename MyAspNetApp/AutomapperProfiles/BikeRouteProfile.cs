using AutoMapper;
using DomainLayer.Models;
using FishingAndCyclingApp.DTOs;

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
