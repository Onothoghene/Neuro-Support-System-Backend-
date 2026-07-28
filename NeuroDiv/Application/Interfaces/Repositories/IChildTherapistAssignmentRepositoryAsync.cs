using Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IChildTherapistAssignmentRepositoryAsync : IGenericRepositoryAsync<ChildTherapistAssignment>
    {
        Task<ChildTherapistAssignment?> GetActiveAssignmentAsync(Guid childId, Guid therapistId);
        Task<ChildTherapistAssignment?> GetByIdAsync(Guid id);
    }
}
