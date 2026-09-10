using Application.DTOs.SessionOccurrence;
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
    public class GetSessionOccurrenceQuery : IRequest<Response<SessionOccurrenceVM>>
    {
        public Guid Id { get; set; }

        public class GetSessionOccurrenceQueryHandler(ISessionOccurrenceRepositoryAsync occurrenceRepository,
                                                      IAuthenticatedUserService authenticatedUser,
                                                      IMapper mapper) 
              : IRequestHandler<GetSessionOccurrenceQuery, Response<SessionOccurrenceVM>>
        {
            private readonly ISessionOccurrenceRepositoryAsync _occurrenceRepository = occurrenceRepository;
            private readonly IAuthenticatedUserService _authenticatedUser = authenticatedUser;
            private readonly IMapper _mapper = mapper;

            public async Task<Response<SessionOccurrenceVM>> Handle(GetSessionOccurrenceQuery query, CancellationToken cancellationToken)
            {
                var occurrence = await _occurrenceRepository.GetByIdWithDetailsAsync(query.Id) ?? 
                                 throw new ApiException("Session occurrence could not be found.");

                var result = _mapper.Map<SessionOccurrenceVM>(occurrence);

                return new Response<SessionOccurrenceVM>(result, "Occurrence retrieved successfully.");
            }
        }
    }
}
