using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class ParentProfileRepositoryAsync : GenericRepositoryAsync<ParentProfile>, IParentProfileRepositoryAsync
    {
        private readonly DbSet<ParentProfile> _parentProfile;

        public ParentProfileRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _parentProfile = dbContext.Set<ParentProfile>();
        }

        public Task<ChildParent> AddChildParentAsync(ChildParent childParent)
        {
            throw new NotImplementedException();
        }

        public Task<ParentProfile?> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
