using Application.DTOs.Session;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class SessionProfile : Profile
    {
        public SessionProfile()
        {
            // Session → SessionVM
            CreateMap<Session, SessionVM>()
                .ForMember(dest => dest.Type,
                    opt => opt.MapFrom(src => src.Type.ToString()))
                .ForMember(dest => dest.Status,
                    opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.DurationLabel,
                    opt => opt.MapFrom(src => src.SessionDuration != null
                        ? src.SessionDuration.Label : "Custom"))
                .ForMember(dest => dest.TherapistFirstName,
                    opt => opt.MapFrom(src => src.Therapist.FirstName))
                .ForMember(dest => dest.TherapistLastName,
                    opt => opt.MapFrom(src => src.Therapist.LastName))
                .ForMember(dest => dest.RecurrencePattern,
                    opt => opt.MapFrom(src => src.RecurrencePattern.HasValue
                        ? src.RecurrencePattern.ToString() : null))
                .ForMember(dest => dest.CancellationReason,
                    opt => opt.MapFrom(src => src.CancellationReason.HasValue
                        ? src.CancellationReason.ToString() : null))
                .ForMember(dest => dest.NoShowType,
                    opt => opt.MapFrom(src => src.NoShowType.HasValue
                        ? src.NoShowType.ToString() : null))
                .ForMember(dest => dest.ChildRecords,
                    opt => opt.MapFrom(src => src.ChildSessionRecords));

            // Session → SessionSummaryVM
            CreateMap<Session, SessionSummaryVM>()
                .ForMember(dest => dest.Type,
                    opt => opt.MapFrom(src => src.Type.ToString()))
                .ForMember(dest => dest.Status,
                    opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.DurationLabel,
                    opt => opt.MapFrom(src => src.SessionDuration != null
                        ? src.SessionDuration.Label : "Custom"))
                .ForMember(dest => dest.ChildCount,
                    opt => opt.MapFrom(src => src.ChildSessionRecords.Count));

        }
    }
}
