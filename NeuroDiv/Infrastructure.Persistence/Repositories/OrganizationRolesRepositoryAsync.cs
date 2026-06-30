using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using System;
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

        public async Task<OrganizationRoles> GetByIdAsync(string Id)
        {
            Guid IdGuid = Guid.Parse(Id);

            return await _organizationRoles.Where(x => x.Id == IdGuid && x.IsDeleted == false)
                                           .FirstOrDefaultAsync();
        }

        public async Task<OrganizationRoles> GetByOrganizationIdAsync(string organizationId)
        {
            Guid organizationIdGuid = Guid.Parse(organizationId);
            return await _organizationRoles.Where(x => x.OrganizationId == organizationIdGuid && x.IsDeleted == false)
                                           .FirstOrDefaultAsync();
        }

    }
}
