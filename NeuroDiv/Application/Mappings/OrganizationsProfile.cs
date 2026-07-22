using Application.DTOs.Organizations;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class OrganizationProfile : Profile
    {
        public OrganizationProfile()
        {
            CreateMap<Organizations, OrganizationsVM>();

        }
    }
}
