using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface ISessionOccurrenceRepositoryAsync : IGenericRepositoryAsync<SessionOccurrence>
    {
        Task<SessionOccurrence?> GetByIdWithDetailsAsync(Guid Id);
        Task<SessionOccurrence?> GetByIdAsync(Guid id);
        Task<List<SessionOccurrence>> GetAllAsync(Guid? sessionClassId, Guid? therapistId, Guid? childProfileId,
                                                  SessionStatus? status, DateTime? fromDate, DateTime? toDate);
        Task<SessionOccurrence?> GetLatestByClassIdAsync(Guid sessionClassId);
        Task<bool> ExistsForClassAndDateAsync(Guid sessionClassId, DateTime date);
    }
}
