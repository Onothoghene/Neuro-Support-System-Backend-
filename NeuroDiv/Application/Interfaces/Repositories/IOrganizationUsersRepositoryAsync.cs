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
    }
}
