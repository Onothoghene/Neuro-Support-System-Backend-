using Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IOrganizationRolesRepositoryAsync : IGenericRepositoryAsync<OrganizationRoles>
    {
        Task<OrganizationRoles> GetByIdAsync(Guid Id);
        Task<OrganizationRoles> GetByOrganizationIdAsync(Guid organizationId);
        Task<OrganizationRoles?> GetByNameAndOrgAsync(string name, Guid organizationId);
    }
}
