using Application.DTOs.Session;
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
    public class GetSessionsQuery : IRequest<Response<List<SessionSummaryVM>>>
    {
        public Guid? OrganizationId { get; set; }
        public Guid? TherapistId { get; set; }
        public Guid? ChildProfileId { get; set; }
        public SessionStatus? Status { get; set; }
        public SessionType? Type { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

        public class GetSessionsQueryHandler(ISessionRepositoryAsync sessionRepository,
                                           IAuthenticatedUserService authenticatedUser,
                                           IMapper mapper) 
              : IRequestHandler<GetSessionsQuery, Response<List<SessionSummaryVM>>>
        {
            private readonly ISessionRepositoryAsync _sessionRepository = sessionRepository;
            private readonly IAuthenticatedUserService _authenticatedUser = authenticatedUser;
            private readonly IMapper _mapper = mapper;

            public async Task<Response<List<SessionSummaryVM>>> Handle(GetSessionsQuery query, CancellationToken cancellationToken)
            {
                var sessions = await _sessionRepository.GetAllAsync(query.OrganizationId, query.TherapistId,
                                                                    query.ChildProfileId, query.Status,
                                                                    query.Type,query.FromDate,query.ToDate);

                var result = _mapper.Map<List<SessionSummaryVM>>(sessions);

                return new Response<List<SessionSummaryVM>>(result, $"{result.Count} session(s) found.");
            }
        }
    }
}
