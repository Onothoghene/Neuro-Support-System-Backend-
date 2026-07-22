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

        public async Task<List<OrganizationUsers>> GetMembersAsync(Guid organizationId, string? roleName, bool? isActive, string? searchTerm, DateTime? joinedFrom, DateTime? joinedTo)
        {
            var query = _organizationUsers.Where(o => o.OrganizationId == organizationId && !o.IsDeleted)
                                          .Include(o => o.User)
                                          .ThenInclude(u => u.OrganizationUserRoles
                                          .Where(r => r.OrganizationId == organizationId))
                                          .ThenInclude(r => r.OrganizationRoles)
                                          .AsQueryable();

            // Filter by active status
            if (isActive.HasValue) query = query.Where(o => o.IsActive == isActive.Value);

            // Filter by role name
            if (!string.IsNullOrWhiteSpace(roleName))
                query = query.Where(o => o.User.OrganizationUserRoles
                             .Any(r => r.OrganizationId == organizationId && r.OrganizationRoles.Name == roleName));

            // Search by name or email
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var term = searchTerm.ToLower();
                query = query.Where(o => o.User.FirstName.Contains(term, StringComparison.CurrentCultureIgnoreCase) ||
                                    o.User.LastName.Contains(term, StringComparison.CurrentCultureIgnoreCase) ||
                                    o.User.LastName.Contains(term, StringComparison.CurrentCultureIgnoreCase) ||
                                    o.User.Email.Contains(term, StringComparison.CurrentCultureIgnoreCase));
            }

            // Filter by join date range
            if (joinedFrom.HasValue)
                query = query.Where(o => o.JoinedAt >= joinedFrom.Value);

            if (joinedTo.HasValue)
                query = query.Where(o => o.JoinedAt <= joinedTo.Value);

            return await query.ToListAsync();
        }
    }
}
