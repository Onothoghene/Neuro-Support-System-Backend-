using Application.DTOs.OrganizationUsersInvite;
using Application.Features.OrganizationUsersInvite.Command;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class OrganizationUsersInviteProfile : Profile
    {
        public OrganizationUsersInviteProfile()
        {
            CreateMap<AddOrUpdateOrganizationUsersInviteCommand, OrganizationUsersInvite>();

            CreateMap<OrganizationUsersInvite, OrganizationUserInviteVM>();

        }
    }
}
