using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IOrganizationUserRolesRepositoryAsync : IGenericRepositoryAsync<OrganizationUserRoles>
    {
        Task<List<OrganizationUserRoles>> GetUserRolesInOrganization(Guid userId, Guid organizationId);
        Task<List<OrganizationUserRoles>> GetByOrganizationId(Guid organizationId);
        Task<OrganizationUserRoles?> GetCurrentClinicOwnerAsync(Guid organizationId);
        Task<OrganizationUserRoles?> GetUserRoleInOrgAsync(Guid userId, Guid organizationId);
    }
}
