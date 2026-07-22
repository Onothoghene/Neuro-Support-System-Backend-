using Application.DTOs.OrganizationUsers;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class OrganizationUsersProfile : Profile
    {
        public OrganizationUsersProfile()
        {
            CreateMap<OrganizationUsers, OrganizationUsersVM>();

        }
    }
}
