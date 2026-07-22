using Application.DTOs.OrganizationUsers;
using Application.Exceptions;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using AutoMapper;
using Domain.Defaults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.OrganizationUsers.Query
{
    public class GetOrganizationMembersQuery : IRequest<Response<List<OrganizationUsersVM>>>
    {
        public Guid OrganizationId { get; set; }

        // Filters
        public string? RoleName { get; set; }
        public bool? IsActive { get; set; }
        public string? SearchTerm { get; set; }        // searches name and email
        public DateTime? JoinedFrom { get; set; }
        public DateTime? JoinedTo { get; set; }

        public class GetOrganizationMembersQueryHandler : IRequestHandler<GetOrganizationMembersQuery, Response<List<OrganizationUsersVM>>>
        {
            private readonly IMapper _mapper;
            private readonly IOrganizationUsersRepositoryAsync _orgUserRepository;
            private readonly IAuthenticatedUserService _authenticatedUser;
            private readonly IOrganizationPermissionService _permissionService;

            public GetOrganizationMembersQueryHandler(IOrganizationUsersRepositoryAsync orgUserRepository, IMapper mapper,
                                                      IAuthenticatedUserService authenticatedUser,
                                                      IOrganizationPermissionService permissionService)
            { 
                _mapper = mapper;
            }
            public async Task<Response<List<OrganizationUsersVM>>> Handle(GetOrganizationMembersQuery query, CancellationToken cancellationToken)
            {
                // Any org member can view the member list
                await _permissionService.EnsureHasRoleAsync(_authenticatedUser.UserId, query.OrganizationId,
                                    DefaultOrganizationRoles.ClinicOwner, DefaultOrganizationRoles.ClinicAdmin,
                                    DefaultOrganizationRoles.LeadTherapist, DefaultOrganizationRoles.Therapist);

                var members = await _orgUserRepository.GetMembersAsync(query.OrganizationId, query.RoleName,
                                                                       query.IsActive, query.SearchTerm,
                                                                       query.JoinedFrom, query.JoinedTo);

                if (members == null) throw new ApiException($"The requested organization user could not be found.");
                return new Response<List<OrganizationUsersVM>>(_mapper.Map<List<OrganizationUsersVM>>(members), "successful");
            }
        }
    }
}

