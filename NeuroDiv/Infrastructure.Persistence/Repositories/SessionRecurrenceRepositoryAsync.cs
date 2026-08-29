using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class SessionRecurrenceRepositoryAsync : GenericRepositoryAsync<SessionRecurrence>, ISessionRecurrenceRepositoryAsync
    {
        private readonly DbSet<SessionRecurrence> _SessionRecurrence;

        public SessionRecurrenceRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _SessionRecurrence = dbContext.Set<SessionRecurrence>();
        }

        public Task<List<SessionRecurrence>> GetBySeriesIdAsync(Guid seriesId)
        {
            return _SessionRecurrence.Where(r => r.SeriesId == seriesId && !r.IsDeleted)
                                     .ToListAsync();
        }
    }
}
