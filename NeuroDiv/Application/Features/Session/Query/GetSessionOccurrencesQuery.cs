using Application.DTOs.SessionOccurrence;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using AutoMapper;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Session.Query
{
    public class GetSessionOccurrencesQuery : IRequest<Response<List<SessionOccurrenceSummaryVM>>>
    {
        public Guid? SessionClassId { get; set; }
        public Guid? TherapistId { get; set; }
        public Guid? ChildProfileId { get; set; }
        public SessionStatus? Status { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

        public class GetSessionOccurrencesQueryHandler(ISessionOccurrenceRepositoryAsync occurrenceRepository,
                                                       IAuthenticatedUserService authenticatedUser,
                                                       IMapper mapper) 
              : IRequestHandler<GetSessionOccurrencesQuery, Response<List<SessionOccurrenceSummaryVM>>>
        {
            private readonly ISessionOccurrenceRepositoryAsync _occurrenceRepository = occurrenceRepository;
            private readonly IAuthenticatedUserService _authenticatedUser = authenticatedUser;
            private readonly IMapper _mapper = mapper;

            public async Task<Response<List<SessionOccurrenceSummaryVM>>> Handle(GetSessionOccurrencesQuery query, CancellationToken cancellationToken)
            {
                var occurrences = await _occurrenceRepository.GetAllAsync(query.SessionClassId, query.TherapistId,
                                                                          query.ChildProfileId, query.Status,
                                                                          query.FromDate, query.ToDate);

                var result = _mapper.Map<List<SessionOccurrenceSummaryVM>>(occurrences);

                return new Response<List<SessionOccurrenceSummaryVM>>(result, $"{result.Count} session(s) found.");
            }
        }
    }
}
