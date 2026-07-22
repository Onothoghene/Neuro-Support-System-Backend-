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
    public class OrganizationRolesRepositoryAsync : GenericRepositoryAsync<OrganizationRoles>, IOrganizationRolesRepositoryAsync
    {
        private readonly DbSet<OrganizationRoles> _organizationRoles;

        public OrganizationRolesRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _organizationRoles = dbContext.Set<OrganizationRoles>();
        }

        public async Task<OrganizationRoles> GetByIdAsync(Guid Id)
        {
            return await _organizationRoles.Where(x => x.Id == Id && x.IsDeleted == false)
                                           .FirstOrDefaultAsync();
        }

        public async Task<List<OrganizationRoles>> GetByOrganizationIdAsync(Guid organizationId)
        {
            return await _organizationRoles.Where(x => x.OrganizationId == organizationId && x.IsDeleted == false)
                                           .OrderBy(r => r.IsDefault)
                                           .ThenBy(r => r.Name)
                                           .ToListAsync();
                                           
        }

        // Gets an org role by name scoped to a specific org
        public async Task<OrganizationRoles?> GetByNameAndOrgAsync(string name, Guid organizationId)
        {
            return await _organizationRoles.FirstOrDefaultAsync(r => r.Name == name
                                                               && r.OrganizationId == organizationId
                                                               && !r.IsDeleted);
        }

    }
}
