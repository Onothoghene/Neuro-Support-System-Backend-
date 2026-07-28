using Application.DTOs.ChildProfile;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.ChildProfile.Query
{
    public class GetChildrenQuery : IRequest<Response<List<ChildProfileSummaryVM>>>
    {
        // Filters
        public Guid? OrganizationId { get; set; }
        public Guid? TherapistId { get; set; }
        public Guid? DiagnosisTypeId { get; set; }
        public bool? IsActive { get; set; }
        public string? SearchTerm { get; set; }

        public class GetChildrenQueryHandler(IChildProfileRepositoryAsync childProfileRepository, IMapper mapper,
                                             IAuthenticatedUserService authenticatedUser)
               : IRequestHandler<GetChildrenQuery, Response<List<ChildProfileSummaryVM>>>
        {
            private readonly IChildProfileRepositoryAsync _childProfileRepository = childProfileRepository;
            private readonly IAuthenticatedUserService _authenticatedUser = authenticatedUser;
            private readonly IMapper _mapper = mapper;

            public async Task<Response<List<ChildProfileSummaryVM>>> Handle(GetChildrenQuery query, CancellationToken cancellationToken)
            {
                var children = await _childProfileRepository.GetAllAsync(
                query.OrganizationId,
                query.TherapistId,
                query.DiagnosisTypeId,
                query.IsActive,
                query.SearchTerm);

                var result = _mapper.Map<List<ChildProfileSummaryVM>>(children);

                return new Response<List<ChildProfileSummaryVM>>(result, $"{result.Count} child profile(s) found.");
            }
        }
    }
}

