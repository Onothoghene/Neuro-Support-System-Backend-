using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class ChildParentRepositoryAsync : GenericRepositoryAsync<ChildParent>, IChildParentRepositoryAsync
    {
        private readonly DbSet<ChildParent> _childParent;

        public ChildParentRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _childParent = dbContext.Set<ChildParent>();
        }

    }
}
