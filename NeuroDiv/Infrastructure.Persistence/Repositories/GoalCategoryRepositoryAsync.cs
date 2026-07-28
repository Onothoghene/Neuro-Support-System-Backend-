using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class GoalCategoryRepositoryAsync : GenericRepositoryAsync<GoalCategory>, IGoalCategoryRepositoryAsync
    {
        private readonly DbSet<GoalCategory> _goalCategory;

        public GoalCategoryRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _goalCategory = dbContext.Set<GoalCategory>();
        }

    }
}
