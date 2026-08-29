using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface ISessionRecurrenceRepositoryAsync : IGenericRepositoryAsync<SessionRecurrence>
    {
        Task<List<SessionRecurrence>> GetBySeriesIdAsync(Guid seriesId);
    }
}
