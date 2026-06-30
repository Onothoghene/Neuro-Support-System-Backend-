using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IOrganizationUsersInviteRepositoryAsync : IGenericRepositoryAsync<OrganizationUsersInvite>
    {
        Task<OrganizationUsersInvite> GetByIdAsync(string Id);
        Task<List<OrganizationUsersInvite>> GetByOrganizationIdAsync(string organizationId);
    }
}
