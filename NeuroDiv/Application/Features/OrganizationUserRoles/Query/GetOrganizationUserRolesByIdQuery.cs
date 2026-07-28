using Application.Exceptions;
using Application.Wrappers;
using AutoMapper;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Application.Interfaces.Repositories;
using Application.DTOs.Comments;
using System;

namespace Application.Features.OrganizationUserRoles.Query
{
    public class GetOrganizationUserRolesByIdQuery : IRequest<Response<CommentVM>>
    {
        public Guid Id { get; set; }

        public class GetOrganizationUserRolesByIdQueryHandler : IRequestHandler<GetOrganizationUserRolesByIdQuery, Response<CommentVM>>
        {
            private readonly ICommentRepositoryAsync _commentRepository;
            private readonly IMapper _mapper;

            public GetOrganizationUserRolesByIdQueryHandler(ICommentRepositoryAsync commentRepository, IMapper mapper)
            {
                _commentRepository = commentRepository;
                _mapper = mapper;
            }
            public async Task<Response<CommentVM>> Handle(GetOrganizationUserRolesByIdQuery query, CancellationToken cancellationToken)
            {
                var response = await _commentRepository.GetByIdAsync(query.Id);
                if (response == null) throw new ApiException($"The requested comment could not be found.");
                return new Response<CommentVM>(_mapper.Map<CommentVM>(response), "successful");
            }
        }
    }
}

