using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class GoalProgressLogRepositoryAsync : GenericRepositoryAsync<GoalProgressLog>, IGoalProgressLogRepositoryAsync
    {
        private readonly DbSet<GoalProgressLog> _GoalProgressLog;

        public GoalProgressLogRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _GoalProgressLog = dbContext.Set<GoalProgressLog>();
        }

      
    }
}
