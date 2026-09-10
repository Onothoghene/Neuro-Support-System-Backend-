using Application.DTOs.Session;
using AutoMapper;
using Domain.Entities;
using System.Linq;

namespace Application.Mappings
{
    public class SessionRecurrenceRuleProfile : Profile
    {
        public SessionRecurrenceRuleProfile()
        {
            CreateMap<SessionRecurrenceRule, RecurrenceRuleVM>()
           .ForMember(dest => dest.DaysOfWeek,
               opt => opt.MapFrom(src => src.GetDaysOfWeek().Select(d => d.ToString()).ToList()))
           .ForMember(dest => dest.DurationLabel,
               opt => opt.MapFrom(src => src.SessionDuration != null ? src.SessionDuration.Label : "Custom"));

        }
    }
}
