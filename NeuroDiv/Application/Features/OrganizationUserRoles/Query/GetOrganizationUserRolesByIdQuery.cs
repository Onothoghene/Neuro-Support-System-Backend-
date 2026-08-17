using Application.DTOs.Comments;
using Application.DTOs.Users;
using Application.Exceptions;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.OrganizationUserRoles.Query
{
    public class GetOrganizationUserRolesByIdQuery : IRequest<Response<UserDetailsVM>>
    {
        public Guid roleId { get; set; }

        public class GetOrganizationUserRolesByIdQueryHandler(IOrganizationUserRolesRepositoryAsync organizationUserRoles, IMapper mapper) : IRequestHandler<GetOrganizationUserRolesByIdQuery, Response<UserDetailsVM>>
        {
            private readonly IOrganizationUserRolesRepositoryAsync _organizationUserRoles = organizationUserRoles;
            private readonly IMapper _mapper = mapper;

            public async Task<Response<UserDetailsVM>> Handle(GetOrganizationUserRolesByIdQuery query, CancellationToken cancellationToken)
            {
                var response = await _organizationUserRoles.GetByIdAsync(query.roleId) ?? 
                               throw new ApiException($"The requested role could not be found.");
                return new Response<UserDetailsVM>(_mapper.Map<UserDetailsVM>(response), "successful");
            }
        }
    }
}

