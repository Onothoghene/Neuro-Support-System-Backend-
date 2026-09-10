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
    public class GetSessionClassQuery : IRequest<Response<SessionClassVM>>
    {
        public Guid Id { get; set; }

        public class GetSessionClassQueryHandler(ISessionClassRepositoryAsync sessionRepository,
                                                 IAuthenticatedUserService authenticatedUser,
                                                 IMapper mapper) 
              : IRequestHandler<GetSessionClassQuery, Response<SessionClassVM>>
        {
            private readonly ISessionClassRepositoryAsync _sessionRepository = sessionRepository;
            private readonly IAuthenticatedUserService _authenticatedUser = authenticatedUser;
            private readonly IMapper _mapper = mapper;

            public async Task<Response<SessionClassVM>> Handle(GetSessionClassQuery query, CancellationToken cancellationToken)
            {
                var session = await _sessionRepository.GetByIdWithDetailsAsync(query.Id) ?? 
                              throw new ApiException("Session could not be found.");

                var result = _mapper.Map<SessionClassVM>(session);

                return new Response<SessionClassVM>(result, "Session retrieved successfully.");
            }
        }
    }
}
