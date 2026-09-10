using Application.DTOs.SessionOccurrence;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class SessionNoShowProfile : Profile
    {
        public SessionNoShowProfile()
        {
            CreateMap<SessionNoShow, SessionNoShowVM>()
                .ForMember(dest => dest.NoShowType, opt => opt.MapFrom(src => src.NoShowType.ToString()));
        }
    }
}
