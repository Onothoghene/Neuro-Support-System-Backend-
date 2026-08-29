using Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IChildSessionRecordRepositoryAsync : IGenericRepositoryAsync<ChildSessionRecord>
    {
        Task<ChildSessionRecord?> GetBySessionAndChildAsync(Guid sessionId, Guid childProfileId);
    }
}
