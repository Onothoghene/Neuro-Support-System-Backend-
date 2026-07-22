using Application.DTOs.OrganizationRoles;
using Application.Features.OrganizationRoles.Command;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class OrganizationRolesProfile : Profile
    {
        public OrganizationRolesProfile()
        {
            CreateMap<AddOrUpdateOrganizationRolesCommand, OrganizationRoles>()
                     .ForMember(dest => dest.IsDefault, opt => opt.MapFrom(src => false))
                     .ForMember(dest => dest.Id, opt => opt.Ignore())
                    //.ForMember(dest => dest.IsDefault, opt => opt.Ignore())
                     .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                     .ForMember(dest => dest.Created, opt => opt.Ignore());

            CreateMap<OrganizationRoles, OrganizationRolesVM>();

        }
    }
}
