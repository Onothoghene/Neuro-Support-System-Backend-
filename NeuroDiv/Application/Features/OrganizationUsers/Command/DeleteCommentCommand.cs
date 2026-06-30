using Application.Exceptions;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using System.Transactions;

namespace Application.Features.OrganizationUsers.Command
{
    public class DeleteOrganizationUsersCommand : IRequest<Response<bool>>
    {
        public int organizationUserId { get; set; }

        public class DeleteOrganizationUsersCommandHandler : IRequestHandler<DeleteOrganizationUsersCommand, Response<bool>>
        {
            private readonly ICommentRepositoryAsync _commentRepository;

            public DeleteOrganizationUsersCommandHandler(IUserProfileRepositoryAsync userProfile,
                                              ICommentRepositoryAsync commentRepository)
            {
                _commentRepository = commentRepository;
            }

            public async Task<Response<bool>> Handle(DeleteOrganizationUsersCommand command, CancellationToken cancellationToken)
            {
                using (var ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    var data = await _commentRepository.GetByIdAsync(command.organizationUserId) ?? 
                                                            throw new ApiException($"The requested organization user could not be found.");

                    //data.IsDeleted = true;
                    await _commentRepository.DeleteAsync(data);

                    ts.Complete();
                    return new Response<bool>(true, "Organization User deleted successfully");

                }
            }
        }
    }
}

