using Application.Exceptions;
using Application.Wrappers;
using AutoMapper;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Application.Interfaces.Repositories;
using Application.DTOs.Comments;

namespace Application.Features.Organizations.Query
{
    public class GetOrganizationsByIdQuery : IRequest<Response<CommentVM>>
    {
        public int OrganizationId { get; set; }

        public class GetOrganizationsByIdQueryHandler : IRequestHandler<GetOrganizationsByIdQuery, Response<CommentVM>>
        {
            private readonly ICommentRepositoryAsync _commentRepository;
            private readonly IMapper _mapper;

            public GetOrganizationsByIdQueryHandler(ICommentRepositoryAsync commentRepository, IMapper mapper)
            {
                _commentRepository = commentRepository;
                _mapper = mapper;
            }
            public async Task<Response<CommentVM>> Handle(GetOrganizationsByIdQuery query, CancellationToken cancellationToken)
            {
                var response = await _commentRepository.GetByIdAsync(query.OrganizationId);
                if (response == null) throw new ApiException($"The requested organization could not be found.");
                return new Response<CommentVM>(_mapper.Map<CommentVM>(response), "successful");
            }
        }
    }
}

