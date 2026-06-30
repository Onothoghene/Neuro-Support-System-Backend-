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
    public class OrganizationUserRolesRepositoryAsync : GenericRepositoryAsync<OrganizationUserRoles>, IOrganizationUserRolesRepositoryAsync
    {
        private readonly DbSet<OrganizationUserRoles> _organizationUserRoles;

        public OrganizationUserRolesRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _organizationUserRoles = dbContext.Set<OrganizationUserRoles>();
        }

        public async Task<List<OrganizationUserRoles>> GetUserRolesInOrganization(string userId, string organizationId)
        {
            Guid userGuid = Guid.Parse(userId);
            Guid organizationGuid = Guid.Parse(organizationId);

            return await _organizationUserRoles
                .Where(x => x.UserId == userGuid && x.OrganizationId == organizationGuid)
                .Include(x => x.OrganizationRoles)
                .ToListAsync();
        }

        public async Task<List<OrganizationUserRoles>> GetById(string Id)
        {
            Guid IdGuid = Guid.Parse(Id);

            return await _organizationUserRoles.Where(x => x.UserId == IdGuid)
                                               .Include(x => x.OrganizationRoles)
                                               .ToListAsync();
        }

        public async Task<List<OrganizationUserRoles>> GetByOrganizationId(string organizationId)
        {
            Guid organizationGuid = Guid.Parse(organizationId);
            return await _organizationUserRoles.Where(x => x.OrganizationId == organizationGuid)
                                               .Include(x => x.OrganizationRoles)
                                               .ThenInclude(x => x.Organizations)
                                               .Include(x => x.User)
                                               .ToListAsync();
        }

    }
}
