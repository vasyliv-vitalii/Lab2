using AutoMapper;
using DomainLayer.Models;
using FishingAndCyclingApp.DTOs;

namespace MyAspNetApp.AutomapperProfiles
{
    public class FishingSpotProfile : Profile
    {
        public FishingSpotProfile()
        {
            CreateMap<FishingSpot, FishingSpotDto>();
            CreateMap<CreateUpdateBikeRouteDto, FishingSpot>();
        }
    }
}
