using Application.Exceptions;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using System.Transactions;
using System;

namespace Application.Features.OrganizationRoles.Command
{
    public class DeleteOrganizationRolesCommand : IRequest<Response<bool>>
    {
        public Guid OrganizationRoleId { get; set; }

        public class DeleteOrganizationRolesCommandHandler : IRequestHandler<DeleteOrganizationRolesCommand, Response<bool>>
        {
            private readonly IOrganizationRolesRepositoryAsync _organizationRolesRepository;

            public DeleteOrganizationRolesCommandHandler(IUserProfileRepositoryAsync userProfile,
                                                         IOrganizationRolesRepositoryAsync organizationRolesRepository)
            {
                _organizationRolesRepository = organizationRolesRepository;
            }

            public async Task<Response<bool>> Handle(DeleteOrganizationRolesCommand command, CancellationToken cancellationToken)
            {
                using (var ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    var data = await _organizationRolesRepository.GetByIdAsync(command.OrganizationRoleId) ?? 
                                                            throw new ApiException($"The requested organization role could not be found.");

                    //data.IsDeleted = true;

                    await _organizationRolesRepository.DeleteAsync(data);

                    ts.Complete();
                    return new Response<bool>(true, "The Organization Role deleted successfully");

                }
            }
        }
    }
}
