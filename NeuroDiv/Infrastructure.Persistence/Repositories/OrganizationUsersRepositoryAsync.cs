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
    public class OrganizationUsersRepositoryAsync : GenericRepositoryAsync<OrganizationUsers>, IOrganizationUsersRepositoryAsync
    {
        private readonly DbSet<OrganizationUsers> _organizationUsers;

        public OrganizationUsersRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _organizationUsers = dbContext.Set<OrganizationUsers>();
        }

        public async Task<OrganizationUsers> GetByIdAsync(string Id)
        {
            Guid IdGuid = Guid.Parse(Id);

            return await _organizationUsers.Where(x => x.Id == IdGuid && x.IsDeleted == false)
                                           .FirstOrDefaultAsync();
        }

        public async Task<List<OrganizationUsers>> GetByOrganizationIdAsync(string organizationId)
        {
            Guid organizationIdGuid = Guid.Parse(organizationId);
            return await _organizationUsers.Where(x => x.OrganizationId == organizationIdGuid && x.IsDeleted == false)
                                           .ToListAsync();
        }

        public async Task<OrganizationUsers> GetByUserIdAndOrganizationIdAsync(string userId, string organizationId)
        {
            Guid userIdGuid = Guid.Parse(userId);
            Guid organizationIdGuid = Guid.Parse(organizationId);

            return await _organizationUsers.Where(x => x.UserId == userIdGuid && x.OrganizationId == organizationIdGuid && x.IsDeleted == false)
                                           .FirstOrDefaultAsync();
        }

        public async Task<List<OrganizationUsers>> GetByUserIdAsync(string userId)
        {
            Guid userIdGuid = Guid.Parse(userId);
            return await _organizationUsers.Where(x => x.UserId == userIdGuid && x.IsDeleted == false)
                                           .ToListAsync();
        }

        public async Task<OrganizationUsers?> GetActiveByUserIdAsync(Guid userId)
        {
            return await _organizationUsers.FirstOrDefaultAsync(o => o.UserId == userId
                                                                && o.IsActive
                                                                && !o.IsDeleted);
        }

        public async Task<OrganizationUsers?> GetByUserIdAndOrgIdAsync(Guid userId, Guid organizationId)
        {
            return await _organizationUsers.FirstOrDefaultAsync(o => o.UserId == userId && o.OrganizationId == organizationId && !o.IsDeleted);
        }

        public async Task<List<OrganizationUsers>> GetAllActiveByUserIdAsync(Guid userId)
        {
            return await _organizationUsers.Where(o => o.UserId == userId && o.IsActive && !o.IsDeleted)
                                           .Include(o => o.Organizations)
                                           .Include(o => o.User.OrganizationUserRoles.Where(r => r.OrganizationId == o.OrganizationId))
                                           .ThenInclude(r => r.OrganizationRoles)
                                           .ToListAsync();
        }
    }
}
