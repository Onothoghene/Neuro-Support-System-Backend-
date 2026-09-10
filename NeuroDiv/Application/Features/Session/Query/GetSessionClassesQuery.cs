using Application.DTOs.Session;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Session.Query
{
    public class GetSessionClassesQuery : IRequest<Response<List<SessionClassVM>>>
    {
        public Guid? OrganizationId { get; set; }
        public Guid? TherapistId { get; set; }
        public Guid? ChildProfileId { get; set; }
        public bool? IsActive { get; set; }

        public class GetSessionClassesQueryHandler(ISessionClassRepositoryAsync sessionClassRepository,
                                                   IAuthenticatedUserService authenticatedUser,
                                                   IMapper mapper) 
              : IRequestHandler<GetSessionClassesQuery, Response<List<SessionClassVM>>>
        {
            private readonly ISessionClassRepositoryAsync _sessionClassRepository = sessionClassRepository;
            private readonly IAuthenticatedUserService _authenticatedUser = authenticatedUser;
            private readonly IMapper _mapper = mapper;

            public async Task<Response<List<SessionClassVM>>> Handle(GetSessionClassesQuery query, CancellationToken cancellationToken)
            {
                var sessions = await _sessionClassRepository.GetAllAsync(query.OrganizationId, query.TherapistId,
                                                                         query.ChildProfileId, query.IsActive);

                var result = _mapper.Map<List<SessionClassVM>>(sessions);

                return new Response<List<SessionClassVM>>(result, $"{result.Count} session(s) found.");
            }
        }
    }
}
