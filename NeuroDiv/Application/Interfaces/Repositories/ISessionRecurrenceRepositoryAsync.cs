using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface ISessionRecurrenceRepositoryAsync : IGenericRepositoryAsync<SessionOccurrence>
    {
        Task<List<SessionOccurrence>> GetBySeriesIdAsync(Guid seriesId);
    }
}
