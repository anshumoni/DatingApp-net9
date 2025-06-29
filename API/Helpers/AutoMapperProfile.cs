using API.DTOs;
using API.Entities;
using API.Extension;
using App.Entities;
using AutoMapper;

namespace API.Helpers;

public class AutoMapperProfile:Profile
{
    public AutoMapperProfile()
    {
        CreateMap<AppUser, MemberDto>()
        .ForMember(d=>d.Age,o=>o.MapFrom(s=>s.DateOfBirth.CalculateAge()))
        .ForMember(d => d.PhotoUrl,
        o => o.MapFrom(s => s.Photos.FirstOrDefault(x => x.IsMain)!.Url));
        CreateMap<Photo, PhotoDtos>();
    }
    
}