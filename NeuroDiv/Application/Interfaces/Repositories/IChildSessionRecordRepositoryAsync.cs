using Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IChildSessionRecordRepositoryAsync : IGenericRepositoryAsync<ChildSessionRecord>
    {
        Task<ChildSessionRecord?> GetByOccurrenceAndChildAsync(Guid occurrenceId, Guid childProfileId);
    }
}
