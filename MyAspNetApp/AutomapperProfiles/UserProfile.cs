using AutoMapper;
using DomainLayer.Models;
using FishingAndCyclingApp.DTOs;

namespace MyAspNetApp.AutomapperProfiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDto>();
        CreateMap<CreateUpdateUserDto, User>();
    }
}