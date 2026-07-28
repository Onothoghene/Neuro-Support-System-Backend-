using Application.DTOs.TherapyGoal;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class TherapyGoalProfile : Profile
    {
        public TherapyGoalProfile()
        {
            CreateMap<AddTherapyGoalRequest, TherapyGoal>()
               .ForMember(dest => dest.Id, opt => opt.Ignore())
               .ForMember(dest => dest.ChildProfileId, opt => opt.Ignore())
               .ForMember(dest => dest.GoalCategory, opt => opt.Ignore())
               .ForMember(dest => dest.Status, opt => opt.Ignore())
               .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
               .ForMember(dest => dest.Created, opt => opt.Ignore());

            CreateMap<TherapyGoal, TherapyGoalVM>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.GoalCategory.Name))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

        }

    }
}
