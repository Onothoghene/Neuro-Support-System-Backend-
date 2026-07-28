using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IChildProfileRepositoryAsync : IGenericRepositoryAsync<ChildProfile>
    {
        Task<ChildProfile?> GetByIdAsync(Guid id);
        Task<List<ChildProfile?>> GetAllAsync(Guid? organizationId, Guid? therapistId, Guid? diagnosisTypeId, 
                                             bool? isActive, string? searchTerm);
    }
}
