using Application.Interfaces.Repositories;
using Domain.Seeds;
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
    public class OrganizationUserRolesRepositoryAsync : GenericRepositoryAsync<OrganizationUserRoles>, IOrganizationUserRolesRepositoryAsync
    {
        private readonly DbSet<OrganizationUserRoles> _organizationUserRoles;

        public OrganizationUserRolesRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _organizationUserRoles = dbContext.Set<OrganizationUserRoles>();
        }

        public async Task<List<OrganizationUserRoles>> GetUserRolesInOrganization(Guid userId, Guid organizationId)
        {
            return await _organizationUserRoles
                .Where(x => x.UserId == userId && x.OrganizationId == organizationId)
                .Include(x => x.OrganizationRoles)
                .ToListAsync();
        }

        public async Task<List<OrganizationUserRoles>> GetById(Guid Id)
        {
            return await _organizationUserRoles.Where(x => x.UserId == Id)
                                               .Include(x => x.OrganizationRoles)
                                               .ToListAsync();
        }

        public async Task<List<OrganizationUserRoles>> GetByOrganizationId(Guid organizationId)
        {
            return await _organizationUserRoles.Where(x => x.OrganizationId == organizationId)
                                               .Include(x => x.OrganizationRoles)
                                               .ThenInclude(x => x.Organizations)
                                               .Include(x => x.User)
                                               .ToListAsync();
        }

        // Gets the current Clinic Owner's role assignment record
        public async Task<OrganizationUserRoles?> GetCurrentClinicOwnerAsync(Guid organizationId)
        {
            return await _organizationUserRoles.Include(r => r.OrganizationRoles)
                                            .FirstOrDefaultAsync(r => r.OrganizationId == organizationId
                                       && r.OrganizationRoles.Name == DefaultOrganizationRoles.ClinicOwner
                                       && !r.IsDeleted);
        }

        public async Task<OrganizationUserRoles?> GetUserRoleInOrgAsync(Guid userId, Guid organizationId)
        {
            return await _organizationUserRoles.Include(r => r.OrganizationRoles)
                                               .FirstOrDefaultAsync(r => r.UserId == userId && 
                                               r.OrganizationId == organizationId && !r.IsDeleted);
        }

    }
}
