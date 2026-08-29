using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class ChildSessionRecordRepositoryAsync : GenericRepositoryAsync<ChildSessionRecord>, IChildSessionRecordRepositoryAsync
    {
        private readonly DbSet<ChildSessionRecord> _childSessionRecord;

        public ChildSessionRecordRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _childSessionRecord = dbContext.Set<ChildSessionRecord>();
        }

        public async Task<ChildSessionRecord?> GetBySessionAndChildAsync(Guid sessionId, Guid childProfileId)
        {
            return await _childSessionRecord.Include(r => r.GoalProgressLogs)
                                            .ThenInclude(g => g.TherapyGoal)
                                            .ThenInclude(t => t.GoalCategory)
                                            .FirstOrDefaultAsync(r => r.SessionId == sessionId
                                                                 && r.ChildProfileId == childProfileId
                                                                 && !r.IsDeleted);
        }

    }
}
