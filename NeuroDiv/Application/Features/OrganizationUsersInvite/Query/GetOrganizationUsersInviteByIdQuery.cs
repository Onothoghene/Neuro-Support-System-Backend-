using Application.Exceptions;
using Application.Wrappers;
using AutoMapper;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Application.Interfaces.Repositories;
using Application.DTOs.Comments;
using Application.DTOs.OrganizationUsersInvite;

namespace Application.Features.OrganizationUsersInvite.Query
{
    public class GetOrganizationUsersInviteByIdQuery : IRequest<Response<OrganizationUserInviteVM>>
    {
        public int Id { get; set; }

        public class GetOrganizationUsersInviteByIdQueryHandler : IRequestHandler<GetOrganizationUsersInviteByIdQuery, Response<OrganizationUserInviteVM>>
        {
            private readonly IOrganizationUsersInviteRepositoryAsync _organizationUsersInviteRepository;
            private readonly IMapper _mapper;

            public GetOrganizationUsersInviteByIdQueryHandler(IOrganizationUsersInviteRepositoryAsync organizationUsersInviteRepository, IMapper mapper)
            {
                _organizationUsersInviteRepository = organizationUsersInviteRepository;
                _mapper = mapper;
            }
            public async Task<Response<OrganizationUserInviteVM>> Handle(GetOrganizationUsersInviteByIdQuery query, CancellationToken cancellationToken)
            {
                var response = await _organizationUsersInviteRepository.GetByIdAsync(query.Id);
                if (response == null) throw new ApiException($"The requested organization user invite could not be found.");
                return new Response<OrganizationUserInviteVM>(_mapper.Map<OrganizationUserInviteVM>(response), "successful");
            }
        }
    }
}

