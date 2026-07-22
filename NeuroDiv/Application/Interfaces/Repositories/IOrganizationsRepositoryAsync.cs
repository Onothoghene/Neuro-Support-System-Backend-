using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IOrganizationsRepositoryAsync : IGenericRepositoryAsync<Organizations>
    {
        Task<Organizations> GetByIdAsync(Guid Id);
        Task<List<Organizations>> GetOrganizationsAsync();
        Task<Organizations?> GetByDomainAsync(string domain);
    }
}
