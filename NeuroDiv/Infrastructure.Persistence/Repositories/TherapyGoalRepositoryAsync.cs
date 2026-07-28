using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class TherapyGoalRepositoryAsync : GenericRepositoryAsync<TherapyGoal>, ITherapyGoalRepositoryAsync
    {
        private readonly DbSet<TherapyGoal> _therapyGoals;

        public TherapyGoalRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _therapyGoals = dbContext.Set<TherapyGoal>();
        }

        public Task<TherapyGoal?> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
