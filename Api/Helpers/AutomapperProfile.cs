using System.Linq;
using Api.DTOs;
using Api.Entities;
using Api.Extensions;
using AutoMapper;

namespace Api.Helpers
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<AppUser, MemberDto>()
            .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(
                src=> src.Photos.SingleOrDefault(x => x.IsMain).Url
            ))
            .ForMember(dest => dest.Age, opt => opt.MapFrom(
                src => src.DateOfBirth.CalculateAge()
            ));
            CreateMap<Photo,PhotoDto>();
        }
    }
}