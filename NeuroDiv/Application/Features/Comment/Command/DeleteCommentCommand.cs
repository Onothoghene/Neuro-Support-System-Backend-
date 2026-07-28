using Application.Exceptions;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using System.Transactions;
using System;

namespace Application.Features.Comment.Command
{
    public class DeleteOrganizationRolesCommand : IRequest<Response<bool>>
    {
        public Guid commentId { get; set; }

        public class DeleteCommentCommandHandler : IRequestHandler<DeleteOrganizationRolesCommand, Response<bool>>
        {
            private readonly ICommentRepositoryAsync _commentRepository;

            public DeleteCommentCommandHandler(IUserProfileRepositoryAsync userProfile,
                                              ICommentRepositoryAsync commentRepository)
            {
                _commentRepository = commentRepository;
            }

            public async Task<Response<bool>> Handle(DeleteOrganizationRolesCommand command, CancellationToken cancellationToken)
            {
                using (var ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    var data = await _commentRepository.GetByIdAsync(command.commentId) ?? 
                                                            throw new ApiException($"The requested comment could not be found.");

                    //data.IsDeleted = true;
                    await _commentRepository.DeleteAsync(data);

                    ts.Complete();
                    return new Response<bool>(true, "Food Comment deleted successfully");

                }
            }
        }
    }
}

