using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Persistence.Repositories
{
    public class SessionDurationRepositoryAsync : GenericRepositoryAsync<SessionDuration>, ISessionDurationRepositoryAsync
    {
        private readonly DbSet<SessionDuration> _sessionDuration;

        public SessionDurationRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _sessionDuration = dbContext.Set<SessionDuration>();
        }

      
    }
}
