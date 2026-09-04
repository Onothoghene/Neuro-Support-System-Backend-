using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface ISessionClassRepositoryAsync : IGenericRepositoryAsync<SessionClass>
    {
        Task<SessionClass?> GetById(Guid id);
        Task<SessionClass?> GetByIdLite(Guid id);
        Task<List<SessionClass>> GetAllAsync(Guid? organizationId, Guid? therapistId, Guid? childProfileId,
                                        SessionStatus? status, SessionType? type,
                                        DateTime? fromDate,DateTime? toDate);
        Task<List<SessionClass>> GetBySeriesIdAsync(Guid seriesId);
    }
}
