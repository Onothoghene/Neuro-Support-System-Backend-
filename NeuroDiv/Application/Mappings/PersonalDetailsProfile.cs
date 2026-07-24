using Application.DTOs.Users;
using Application.Mappings.Actions;
using AutoMapper;
using Domain.Entities;
using System;

namespace Application.Mappings
{
    public class PersonalDetailsProfile : Profile
    {
        public PersonalDetailsProfile()
        {
            CreateMap<UserProfile, UserDetailsVM>()
                  .AfterMap<PersonalDetailsMappingAction>();

           }

        public string DateTimeToEpoc(DateTime date)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return Convert.ToInt64((date - epoch).TotalSeconds).ToString();
        }

    }
}
