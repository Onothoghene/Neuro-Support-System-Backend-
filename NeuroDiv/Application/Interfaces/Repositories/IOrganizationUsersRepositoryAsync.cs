using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IOrganizationUsersRepositoryAsync : IGenericRepositoryAsync<OrganizationUsers>
    {
        Task<OrganizationUsers> GetByIdAsync(string Id);
        Task<List<OrganizationUsers>> GetByOrganizationIdAsync(string organizationId);
        Task<OrganizationUsers> GetByUserIdAndOrganizationIdAsync(string userId, string organizationId);
        Task<List<OrganizationUsers>> GetByUserIdAsync(string userId);
        Task<OrganizationUsers?> GetActiveByUserIdAsync(Guid userId);
        
        // Replaces GetActiveByUserIdAsync for the duplicate check
        Task<OrganizationUsers?> GetByUserIdAndOrgIdAsync(Guid userId, Guid organizationId);

        // For fetching all orgs on login
        Task<List<OrganizationUsers>> GetAllActiveByUserIdAsync(Guid userId);

        Task<List<OrganizationUsers>> GetMembersAsync(Guid organizationId, string? roleName, bool? isActive, string? searchTerm, DateTime? joinedFrom, DateTime? joinedTo);
    }
}
