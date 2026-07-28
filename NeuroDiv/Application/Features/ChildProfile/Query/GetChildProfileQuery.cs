using Application.DTOs.ChildProfile;
using Application.Exceptions;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.ChildProfile.Query
{
    public class GetChildProfileQuery : IRequest<Response<ChildProfileVM>>
    {
        public Guid Id { get; set; }

        public class GetChildProfileQueryHandler(IChildProfileRepositoryAsync childProfileRepository, IMapper mapper) 
               : IRequestHandler<GetChildProfileQuery, Response<ChildProfileVM>>
        {
            private readonly IChildProfileRepositoryAsync _childProfileRepository = childProfileRepository;
            private readonly IMapper _mapper = mapper;

            public async Task<Response<ChildProfileVM>> Handle(GetChildProfileQuery query, CancellationToken cancellationToken)
            {
                var child = await _childProfileRepository.GetByIdAsync(query.Id)
                            ?? throw new ApiException("Child profile could not be found.");

                var result = _mapper.Map<ChildProfileVM>(child);

                return new Response<ChildProfileVM>(result, "Child profile retrieved successfully.");
            }
        }
    }
}

