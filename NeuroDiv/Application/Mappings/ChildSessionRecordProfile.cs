using Application.DTOs.ChildSessionRecord;
using Application.DTOs.GoalProgressLog;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class ChildSessionRecordProfile : Profile
    {
        public ChildSessionRecordProfile()
        {
            // ChildSessionRecord → ChildSessionRecordVM
            CreateMap<ChildSessionRecord, ChildSessionRecordVM>()
                .ForMember(dest => dest.ChildFirstName,
                    opt => opt.MapFrom(src => src.ChildProfile.FirstName))
                .ForMember(dest => dest.ChildLastName,
                    opt => opt.MapFrom(src => src.ChildProfile.LastName))
                .ForMember(dest => dest.Engagement,
                    opt => opt.MapFrom(src => src.Engagement.HasValue
                        ? src.Engagement.ToString() : null));

            // GoalProgressLog → GoalProgressLogVM
            CreateMap<GoalProgressLog, GoalProgressLogVM>()
                .ForMember(dest => dest.GoalTitle,
                    opt => opt.MapFrom(src => src.TherapyGoal.Title))
                .ForMember(dest => dest.GoalCategory,
                    opt => opt.MapFrom(src => src.TherapyGoal.GoalCategory.Name))
                .ForMember(dest => dest.StatusUpdate,
                    opt => opt.MapFrom(src => src.StatusUpdate.HasValue
                        ? src.StatusUpdate.ToString() : null));

        }
    }
}
