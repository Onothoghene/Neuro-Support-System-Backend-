using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class TherapistSpecializationRepositoryAsync : GenericRepositoryAsync<TherapistSpecialization>, ITherapistSpecializationRepositoryAsync
    {
        private readonly DbSet<TherapistSpecialization> _therapistSpecializations;

        public TherapistSpecializationRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _therapistSpecializations = dbContext.Set<TherapistSpecialization>();
        }

    }
}
