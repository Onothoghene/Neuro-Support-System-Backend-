using Application.Exceptions;
using Application.Wrappers;
using AutoMapper;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Application.Interfaces.Repositories;
using Application.DTOs.Comments;
using Application.DTOs.OrganizationUsersInvite;
using Application.DTOs.OrganizationUserRoles;
using Application.DTOs.OrganizationUsers;
using Application.DTOs.Organizations;
using System;

namespace Application.Features.Comment.Query
{
    public class GetByIdQuery : IRequest<Response<CommentVM>>
    {
        public Guid commentId { get; set; }

        public class GetCommentByIdQueryHandler : IRequestHandler<GetByIdQuery, Response<CommentVM>>
        {
            private readonly ICommentRepositoryAsync _commentRepository;
            private readonly IMapper _mapper;

            public GetCommentByIdQueryHandler(ICommentRepositoryAsync commentRepository, IMapper mapper)
            {
                _commentRepository = commentRepository;
                _mapper = mapper;
            }
            public async Task<Response<CommentVM>> Handle(GetByIdQuery query, CancellationToken cancellationToken)
            {
                var response = await _commentRepository.GetByIdAsync(query.commentId);
                if (response == null) throw new ApiException($"The requested comment could not be found.");
                return new Response<CommentVM>(_mapper.Map<CommentVM>(response), "successful");
            }
        }
    }
}

