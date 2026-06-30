using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IOrganizationUserRolesRepositoryAsync : IGenericRepositoryAsync<OrganizationUserRoles>
    {
        Task<List<OrganizationUserRoles>> GetUserRolesInOrganization(string userId, string organizationId);
        Task<List<OrganizationUserRoles>> GetById(string Id);
        Task<List<OrganizationUserRoles>> GetByOrganizationId(string organizationId);
    }
}
