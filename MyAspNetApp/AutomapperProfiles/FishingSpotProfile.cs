using AutoMapper;
using DomainLayer.Models;
using MyAspNetApp.DTOs;

namespace MyAspNetApp.AutomapperProfiles
{
    public class FishingSpotProfile : Profile
    {
        public FishingSpotProfile()
        {
            CreateMap<FishingSpot, FishingSpotDto>();
            CreateMap<CreateUpdateFishingSpotDto, FishingSpot>();
        }
    }
}
