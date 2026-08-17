using Application.DTOs.Organizations;
using Application.Exceptions;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using AutoMapper;
using Domain.Seeds;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Organizations.Query
{
    public class GetOrganizationsByIdQuery : IRequest<Response<OrganizationsVM>>
    {
        public Guid OrganizationId { get; set; }

        public class GetOrganizationsByIdQueryHandler : IRequestHandler<GetOrganizationsByIdQuery, Response<OrganizationsVM>>
        {
            private readonly IOrganizationsRepositoryAsync _organizationsRepository;
            private readonly IMapper _mapper;
            private readonly IAuthenticatedUserService _authenticatedUser;
            private readonly IOrganizationPermissionService _permissionService;

            public GetOrganizationsByIdQueryHandler(IOrganizationsRepositoryAsync organizationsRepository, IMapper mapper,
                                        IAuthenticatedUserService authenticatedUser,
                                        IOrganizationPermissionService permissionService)
            {
                _organizationsRepository = organizationsRepository;
                _mapper = mapper;
                _authenticatedUser = authenticatedUser;
                _permissionService = permissionService;
            }
            public async Task<Response<OrganizationsVM>> Handle(GetOrganizationsByIdQuery query, CancellationToken cancellationToken)
            {
                // Any org member can view org details
                await _permissionService.EnsureHasRoleAsync(_authenticatedUser.UserId, query.OrganizationId,
                                DefaultOrganizationRoles.ClinicOwner, DefaultOrganizationRoles.ClinicAdmin,
                                DefaultOrganizationRoles.LeadTherapist, DefaultOrganizationRoles.Therapist);

                //if (!_permissionService.HasPermission(_authenticatedUser.UserId, query.OrganizationId, "ViewOrganization"))
                //{
                //    throw new ApiException($"You do not have permission to view this organization.");
                //}

                var response = await _organizationsRepository.GetByIdAsync(query.OrganizationId) ??
                               throw new ApiException($"The requested organization could not be found.");
                return new Response<OrganizationsVM>(_mapper.Map<OrganizationsVM>(response), "successful");
            }
        }
    }
}

