using Application.Interfaces;
using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class DiagnosisTypeRepositoryAsync : GenericRepositoryAsync<DiagnosisType>, IDiagnosisTypeRepositoryAsync
    {
        private readonly DbSet<DiagnosisType> _diagnosisType;

        public DiagnosisTypeRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _diagnosisType = dbContext.Set<DiagnosisType>();
        }
    }
}
