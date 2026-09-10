using Application.DTOs.Session;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class SessionOnlineDetailsProfile : Profile
    {
        public SessionOnlineDetailsProfile()
        {
            CreateMap<SessionOnlineDetails, OnlineSessionDetailsVM>()
                .ForMember(dest => dest.Platform, opt => opt.MapFrom(src => src.Platform.ToString()));

            CreateMap<OnlineSessionDetailsRequest, SessionOnlineDetails>(); ;
        }
    }
}
