using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class SessionNoShowRepositoryAsync : GenericRepositoryAsync<SessionNoShow>, ISessionNoShowRepositoryAsync
    {
        private readonly DbSet<SessionNoShow> _SessionNoShow;

        public SessionNoShowRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _SessionNoShow = dbContext.Set<SessionNoShow>();
        }

    }
}
