using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IOrganizationUsersInviteRepositoryAsync : IGenericRepositoryAsync<OrganizationUsersInvite>
    {
        Task<OrganizationUsersInvite> GetByIdAsync(Guid Id);
        Task<List<OrganizationUsersInvite>> GetByOrganizationIdAsync(Guid organizationId);
        Task<OrganizationUsersInvite?> GetPendingInviteByEmailAndOrgAsync(string email, Guid organizationId);
        Task<OrganizationUsersInvite?> GetByTokenAsync(string token);
    }
}
