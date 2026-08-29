using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class SessionRepositoryAsync : GenericRepositoryAsync<Session>, ISessionRepositoryAsync
    {
        private readonly DbSet<Session> _session;

        public SessionRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _session = dbContext.Set<Session>();
        }

        public async Task<Session?> GetById(Guid id)
        {
            return await _session.Include(s => s.Therapist)
                                 .Include(s => s.SessionDuration)
                                 .Include(s => s.ChildSessionRecords)
                                 .ThenInclude(r => r.ChildProfile)
                                 .Include(s => s.ChildSessionRecords)
                                 .ThenInclude(r => r.GoalProgressLogs)
                                 .ThenInclude(g => g.TherapyGoal)
                                 .ThenInclude(t => t.GoalCategory)
                                 .FirstOrDefaultAsync(s => s.Id == id && !s.IsDeleted);
        }

        public async Task<List<Session>> GetAllAsync(Guid? organizationId, Guid? therapistId, Guid? childProfileId,
                                                    SessionStatus? status, SessionType? type,
                                                    DateTime? fromDate, DateTime? toDate)
        {
            var query = _session.Where(s => !s.IsDeleted)
                                .Include(s => s.Therapist)
                                .Include(s => s.SessionDuration)
                                .Include(s => s.ChildSessionRecords)
                                .ThenInclude(r => r.ChildProfile)
                                .AsQueryable();

            if (organizationId.HasValue)
                query = query.Where(s => s.OrganizationId == organizationId.Value);

            if (therapistId.HasValue)
                query = query.Where(s => s.TherapistId == therapistId.Value);

            if (childProfileId.HasValue)
                query = query.Where(s => s.ChildSessionRecords
                    .Any(r => r.ChildProfileId == childProfileId.Value));

            if (status.HasValue)
                query = query.Where(s => s.Status == status.Value);

            if (type.HasValue)
                query = query.Where(s => s.Type == type.Value);

            if (fromDate.HasValue)
                query = query.Where(s => s.ScheduledDate >= fromDate.Value);

            if (toDate.HasValue)
                query = query.Where(s => s.ScheduledDate <= toDate.Value);

            return await query
                .OrderBy(s => s.ScheduledDate)
                .ThenBy(s => s.StartTime)
                .ToListAsync();
        }

        public async Task<List<Session>> GetBySeriesIdAsync(Guid seriesId)
        {
            return await _session.Where(s => s.RecurringSeriesId == seriesId && !s.IsDeleted
                                        && s.ScheduledDate >= DateTime.UtcNow.Date)
                                 .OrderBy(s => s.ScheduledDate)
                                 .ToListAsync();
        }

    }
}
