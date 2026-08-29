using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class SessionCancellationRepositoryAsync : GenericRepositoryAsync<SessionCancellation>, ISessionCancellationRepositoryAsync
    {
        private readonly DbSet<SessionCancellation> _SessionCancellation;

        public SessionCancellationRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _SessionCancellation = dbContext.Set<SessionCancellation>();
        }


    }
}
