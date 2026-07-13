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
    public class OrganizationsRepositoryAsync : GenericRepositoryAsync<Organizations>, IOrganizationsRepositoryAsync
    {
        private readonly DbSet<Organizations> _organizations;

        public OrganizationsRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _organizations = dbContext.Set<Organizations>();
        }

        public async Task<Organizations> GetByIdAsync(string Id)
        {
            Guid IdGuid = Guid.Parse(Id);

            return await _organizations.Where(x => x.Id == IdGuid && x.IsDeleted == false)
                                       .Include(x => x.OrganizationUsers)
                                       .FirstOrDefaultAsync();
        }
        
        public async Task<List<Organizations>> GetOrganizationsAsync()
        {
            return await _organizations.Where(x => x.IsDeleted == false)
                                       .Include(x => x.OrganizationUsers)
                                       .ToListAsync();
        }

        public async Task<Organizations?> GetByDomainAsync(string domain)
        {
            return await _organizations.FirstOrDefaultAsync(o => o.Domain.Equals(domain, StringComparison.CurrentCultureIgnoreCase)
                                       && !o.IsDeleted);
        }

    }
}
