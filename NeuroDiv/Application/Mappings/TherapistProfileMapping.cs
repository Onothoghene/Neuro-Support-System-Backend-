using Application.DTOs.File;
using Application.Mappings.Actions;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class TherapistProfileMapping : Profile
    {
        public TherapistProfileMapping()
        {
            CreateMap<TherapistProfile, TherapistProfileVM>();

        }

    }
}
