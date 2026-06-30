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
    public class OrganizationUsersInviteRepositoryAsync : GenericRepositoryAsync<OrganizationUsersInvite>, IOrganizationUsersInviteRepositoryAsync
    {
        private readonly DbSet<OrganizationUsersInvite> _organizationUsersInvite;

        public OrganizationUsersInviteRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _organizationUsersInvite = dbContext.Set<OrganizationUsersInvite>();
        }

        public async Task<OrganizationUsersInvite> GetByIdAsync(string Id)
        {
            Guid IdGuid = Guid.Parse(Id);
            return await _organizationUsersInvite.Where(x => x.Id == IdGuid && x.IsDeleted == false)
                                                 .FirstOrDefaultAsync();
        }

        public async Task<List<OrganizationUsersInvite>> GetByOrganizationIdAsync(string organizationId)
        {
            Guid organizationIdGuid = Guid.Parse(organizationId);
            return await _organizationUsersInvite.Where(x => x.OrganizationId == organizationIdGuid && x.IsDeleted == false)
                                                 .ToListAsync();
        }

    }
}
