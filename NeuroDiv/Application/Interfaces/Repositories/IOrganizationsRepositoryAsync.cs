using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IOrganizationsRepositoryAsync : IGenericRepositoryAsync<Organizations>
    {
        Task<Organizations> GetByIdAsync(string Id);
        Task<List<Organizations>> GetOrganizationsAsync();
    }
}
