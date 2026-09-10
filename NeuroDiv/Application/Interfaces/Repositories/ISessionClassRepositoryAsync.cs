using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface ISessionClassRepositoryAsync : IGenericRepositoryAsync<SessionClass>
    {
        Task<SessionClass?> GetByIdWithDetailsAsync(Guid id);
        Task<List<SessionClass>> GetAllActiveRecurringAsync();
        Task<List<SessionClass>> GetAllAsync(Guid? organizationId, Guid? therapistId, Guid? childProfileId, bool? isActive);
    }
}
