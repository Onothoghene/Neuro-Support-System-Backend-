using Application.Exceptions;
using Application.Wrappers;
using AutoMapper;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Application.Interfaces.Repositories;
using Application.DTOs.OrganizationUsersInvite;
using System.Collections.Generic;
using System;

namespace Application.Features.OrganizationUsersInvite.Query
{
    public class GetOrgUserInviteByOrgIdQuery : IRequest<Response<List<OrganizationUserInviteVM>>>
    {
        public Guid Id { get; set; }

        public class GetOrgUserInviteByOrgIdQueryHandler : IRequestHandler<GetOrgUserInviteByOrgIdQuery, Response<List<OrganizationUserInviteVM>>>
        {
            private readonly IOrganizationUsersInviteRepositoryAsync _organizationUsersInviteRepository;
            private readonly IMapper _mapper;

            public GetOrgUserInviteByOrgIdQueryHandler(IOrganizationUsersInviteRepositoryAsync organizationUsersInviteRepository, IMapper mapper)
            {
                _organizationUsersInviteRepository = organizationUsersInviteRepository;
                _mapper = mapper;
            }
            public async Task<Response<List<OrganizationUserInviteVM>>> Handle(GetOrgUserInviteByOrgIdQuery query, CancellationToken cancellationToken)
            {
                var response = await _organizationUsersInviteRepository.GetByIdAsync(query.Id);
                if (response == null) throw new ApiException($"The requested organization user invite could not be found.");
                return new Response<List<OrganizationUserInviteVM>>(_mapper.Map<List<OrganizationUserInviteVM>>(response), "successful");
            }
        }
    }
}

