using Application.Exceptions;
using Application.Wrappers;
using AutoMapper;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Application.Interfaces.Repositories;
using Application.DTOs.Comments;

namespace Application.Features.OrganizationUsers.Query
{
    public class GetOrganizationUsersByIdQuery : IRequest<Response<CommentVM>>
    {
        public int organizationUserId { get; set; }

        public class GetOrganizationUsersByIdQueryHandler : IRequestHandler<GetOrganizationUsersByIdQuery, Response<CommentVM>>
        {
            private readonly ICommentRepositoryAsync _commentRepository;
            private readonly IMapper _mapper;

            public GetOrganizationUsersByIdQueryHandler(ICommentRepositoryAsync commentRepository, IMapper mapper)
            {
                _commentRepository = commentRepository;
                _mapper = mapper;
            }
            public async Task<Response<CommentVM>> Handle(GetOrganizationUsersByIdQuery query, CancellationToken cancellationToken)
            {
                var response = await _commentRepository.GetByIdAsync(query.organizationUserId);
                if (response == null) throw new ApiException($"The requested organization user could not be found.");
                return new Response<CommentVM>(_mapper.Map<CommentVM>(response), "successful");
            }
        }
    }
}

