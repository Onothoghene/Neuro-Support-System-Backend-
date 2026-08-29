using Application.DTOs.Session;
using Application.Exceptions;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Session.Query
{
    public class GetSessionQuery : IRequest<Response<SessionVM>>
    {
        public Guid Id { get; set; }

        public class GetSessionQueryHandler(ISessionRepositoryAsync sessionRepository,
                                           IAuthenticatedUserService authenticatedUser,
                                           IMapper mapper) 
              : IRequestHandler<GetSessionQuery, Response<SessionVM>>
        {
            private readonly ISessionRepositoryAsync _sessionRepository = sessionRepository;
            private readonly IAuthenticatedUserService _authenticatedUser = authenticatedUser;
            private readonly IMapper _mapper = mapper;

            public async Task<Response<SessionVM>> Handle(GetSessionQuery query, CancellationToken cancellationToken)
            {
                var session = await _sessionRepository.GetById(query.Id) ?? 
                              throw new ApiException("Session could not be found.");

                var result = _mapper.Map<SessionVM>(session);

                return new Response<SessionVM>(result, "Session retrieved successfully.");
            }
        }
    }
}
