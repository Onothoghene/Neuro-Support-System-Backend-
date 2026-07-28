using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class ChildTherapistAssignmentRepositoryAsync(ApplicationDbContext dbContext) : GenericRepositoryAsync<ChildTherapistAssignment>(dbContext), IChildTherapistAssignmentRepositoryAsync
    {
        private readonly DbSet<ChildTherapistAssignment> _childTherapistAssignment = dbContext.Set<ChildTherapistAssignment>();

        public Task<ChildTherapistAssignment?> GetActiveAssignmentAsync(Guid childId, Guid therapistId)
        {
            throw new NotImplementedException();
        }

        public Task<ChildTherapistAssignment?> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
