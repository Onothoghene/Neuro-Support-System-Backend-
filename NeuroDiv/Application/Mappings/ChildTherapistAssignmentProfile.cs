using Application.DTOs.ChildProfile;
using Application.DTOs.ChildTherapistAssignment;
using Application.DTOs.File;
using Application.Mappings.Actions;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class ChildTherapistAssignmentProfile : Profile
    {
        public ChildTherapistAssignmentProfile()
        {
            CreateMap<ChildTherapistAssignment, ChildTherapistAssignmentVM>()
                  .ForMember(dest => dest.AssignmentId,
                      opt => opt.MapFrom(src => src.Id))
                  .ForMember(dest => dest.TherapistId,
                      opt => opt.MapFrom(src => src.TherapistId))
                  .ForMember(dest => dest.FirstName,
                      opt => opt.MapFrom(src => src.Therapist.FirstName))
                  .ForMember(dest => dest.LastName,
                      opt => opt.MapFrom(src => src.Therapist.LastName))
                  .ForMember(dest => dest.AssignmentRole,
                      opt => opt.MapFrom(src => src.Role.ToString()));


        }

    }
}
