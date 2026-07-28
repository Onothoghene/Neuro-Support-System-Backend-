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
    public class ChildProfileRepositoryAsync : GenericRepositoryAsync<ChildProfile>, IChildProfileRepositoryAsync
    {
        private readonly DbSet<ChildProfile> _childProfile;

        public ChildProfileRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _childProfile = dbContext.Set<ChildProfile>();
        }

        public async Task<List<ChildProfile?>> GetAllAsync(Guid? organizationId, Guid? therapistId, Guid? diagnosisTypeId, 
                                                           bool? isActive, string? searchTerm)
        {
            var query = _childProfile.Include(c => c.DiagnosisType)
                                     .Include(c => c.TherapyGoals)
                                     .Where(c => !c.IsDeleted)
                                     .AsQueryable();

            if (organizationId.HasValue)
                query = query.Where(c => c.OrganizationId == organizationId.Value);

            if (therapistId.HasValue)
                query = query.Where(c => c.TherapistAssignments.Any(a => a.TherapistId == therapistId.Value && a.EndDate == null));

            if (diagnosisTypeId.HasValue)
                query = query.Where(c => c.DiagnosisTypeId == diagnosisTypeId.Value);

            if (isActive.HasValue)
                query = query.Where(c => c.IsActive == isActive.Value);

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var term = searchTerm.ToLower();
                query = query.Where(c => c.FirstName.Contains(term, StringComparison.CurrentCultureIgnoreCase) ||
                                    c.LastName.Contains(term, StringComparison.CurrentCultureIgnoreCase));
            }

            return await query.OrderBy(c => c.FirstName).ToListAsync();
        }

        public async Task<ChildProfile?> GetByIdAsync(Guid id)
        {
            return await _childProfile.Include(c => c.DiagnosisType)
                                      .Include(c => c.TherapyGoals)
                                      .ThenInclude(g => g.GoalCategory)
                                      .Include(c => c.TherapistAssignments.Where(a => a.EndDate == null))
                                      .ThenInclude(a => a.Therapist)
                                      .Include(c => c.Parents)
                                      .ThenInclude(cp => cp.ParentProfile)
                                      .FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted);
        }
    }
}
