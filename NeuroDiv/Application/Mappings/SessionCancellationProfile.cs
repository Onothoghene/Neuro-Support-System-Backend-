using Application.DTOs.SessionOccurrence;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class SessionCancellationProfile : Profile
    {
        public SessionCancellationProfile()
        {
            CreateMap<SessionCancellation, SessionCancellationVM>()
                .ForMember(dest => dest.Reason, opt => opt.MapFrom(src => src.Reason.ToString()));
        }
    }
}
