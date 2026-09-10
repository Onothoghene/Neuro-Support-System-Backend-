using Application.DTOs.ChildSessionRecord;
using Application.Features.Session.Command;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class ChildSessionRecordProfile : Profile
    {
        public ChildSessionRecordProfile()
        {
            // ChildSessionRecord -> ChildSessionRecordVM
            CreateMap<ChildSessionRecord, ChildSessionRecordVM>()
                .ForMember(dest => dest.ChildFirstName, opt => opt.MapFrom(src => src.ChildProfile.FirstName))
                .ForMember(dest => dest.ChildLastName, opt => opt.MapFrom(src => src.ChildProfile.LastName))
                .ForMember(dest => dest.Engagement, opt => opt.MapFrom(src => src.Engagement.HasValue ? src.Engagement.ToString() : null));

            CreateMap<AddOrUpdateChildSessionRecordCommand, ChildSessionRecord>();

        }
    }
}
