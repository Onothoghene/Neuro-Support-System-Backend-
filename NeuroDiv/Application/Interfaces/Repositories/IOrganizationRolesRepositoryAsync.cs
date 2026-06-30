using Domain.Entities;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IOrganizationRolesRepositoryAsync : IGenericRepositoryAsync<OrganizationRoles>
    {
        Task<OrganizationRoles> GetByIdAsync(string Id);
        Task<OrganizationRoles> GetByOrganizationIdAsync(string organizationId);
    }
}
