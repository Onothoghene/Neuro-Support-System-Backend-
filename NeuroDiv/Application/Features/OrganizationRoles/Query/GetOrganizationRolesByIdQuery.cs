using Application.Exceptions;
using Application.Wrappers;
using AutoMapper;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Application.Interfaces.Repositories;
using Application.DTOs.Comments;

namespace Application.Features.OrganizationRoles.Query
{
    public class GetOrganizationRolesByIdQuery : IRequest<Response<CommentVM>>
    {
        public int commentId { get; set; }

        public class GetOrganizationRolesByIdQueryHandler : IRequestHandler<GetOrganizationRolesByIdQuery, Response<CommentVM>>
        {
            private readonly ICommentRepositoryAsync _commentRepository;
            private readonly IMapper _mapper;

            public GetOrganizationRolesByIdQueryHandler(ICommentRepositoryAsync commentRepository, IMapper mapper)
            {
                _commentRepository = commentRepository;
                _mapper = mapper;
            }
            public async Task<Response<CommentVM>> Handle(GetOrganizationRolesByIdQuery query, CancellationToken cancellationToken)
            {
                var response = await _commentRepository.GetByIdAsync(query.commentId);
                if (response == null) throw new ApiException($"The requested organization role could not be found.");
                return new Response<CommentVM>(_mapper.Map<CommentVM>(response), "successful");
            }
        }
    }
}

