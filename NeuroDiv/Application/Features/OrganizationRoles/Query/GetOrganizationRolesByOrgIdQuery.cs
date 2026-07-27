using Application.DTOs.OrganizationRoles;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using AutoMapper;
using Domain.Seeds;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.OrganizationRoles.Query
{
    public class GetOrganizationRolesByOrgIdQuery : IRequest<Response<List<OrganizationRolesVM>>>
    {
        public Guid OrganizationId { get; set; }

        public class GetOrganizationRolesByOrgIdQueryHandler : IRequestHandler<GetOrganizationRolesByOrgIdQuery, Response<List<OrganizationRolesVM>>>
        {
            private readonly IMapper _mapper;
            private readonly IOrganizationRolesRepositoryAsync _orgRolesRepository;
            private readonly IAuthenticatedUserService _authenticatedUser;
            private readonly IOrganizationPermissionService _permissionService;

            public GetOrganizationRolesByOrgIdQueryHandler(IOrganizationRolesRepositoryAsync orgRolesRepository,
                                                        IAuthenticatedUserService authenticatedUser, IMapper mapper,
                                                        IOrganizationPermissionService permissionService)
            {
                _orgRolesRepository = orgRolesRepository;
                _authenticatedUser = authenticatedUser;
                _mapper = mapper;
                _permissionService = permissionService;
            }
            public async Task<Response<List<OrganizationRolesVM>>> Handle(GetOrganizationRolesByOrgIdQuery query, CancellationToken cancellationToken)
            {
                await _permissionService.EnsureHasRoleAsync(_authenticatedUser.UserId,query.OrganizationId,
                                    DefaultOrganizationRoles.ClinicOwner, DefaultOrganizationRoles.ClinicAdmin,
                                    DefaultOrganizationRoles.LeadTherapist, DefaultOrganizationRoles.Therapist);

                var roles = await _orgRolesRepository.GetByOrganizationIdAsync(query.OrganizationId);

                return new Response<List<OrganizationRolesVM>>(_mapper.Map<List<OrganizationRolesVM>>(roles), $"{roles.Count} role(s) found.");

            }
        }
    }
}

