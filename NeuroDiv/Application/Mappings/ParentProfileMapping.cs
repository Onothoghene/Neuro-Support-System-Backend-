using Application.DTOs.ParentProfile;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class ParentProfileMapping : Profile
    {
        public ParentProfileMapping()
        {
            CreateMap<AddParentProfileRequest, ParentProfile>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.UserProfileId, opt => opt.Ignore())
                .ForMember(dest => dest.UserProfile, opt => opt.Ignore())
                .ForMember(dest => dest.Children, opt => opt.Ignore())
                .ForMember(dest => dest.IsActive, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.Created, opt => opt.Ignore());

            CreateMap<ParentProfile, ParentProfileVM>();
        }

    }
}
