using Application.DTOs.GoalProgressLog;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class GoalProgressLogProfile : Profile
    {
        public GoalProgressLogProfile()
        {
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
