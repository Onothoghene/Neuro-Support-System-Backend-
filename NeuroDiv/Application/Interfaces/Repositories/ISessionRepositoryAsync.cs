using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface ISessionRepositoryAsync : IGenericRepositoryAsync<Session>
    {
        Task<Session?> GetById(Guid id);
        Task<Session?> GetByIdLite(Guid id)
        Task<List<Session>> GetAllAsync(Guid? organizationId, Guid? therapistId, Guid? childProfileId,
                                        SessionStatus? status, SessionType? type,
                                        DateTime? fromDate,DateTime? toDate);
        Task<List<Session>> GetBySeriesIdAsync(Guid seriesId);
    }
}
