using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class SessionRecurrenceRuleRepositoryAsync : GenericRepositoryAsync<SessionRecurrenceRule>, ISessionRecurrenceRuleRepositoryAsync
    {
        private readonly DbSet<SessionRecurrenceRule> _SessionRecurrenceRule;

        public SessionRecurrenceRuleRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _SessionRecurrenceRule = dbContext.Set<SessionRecurrenceRule>();
        }

    }
}
