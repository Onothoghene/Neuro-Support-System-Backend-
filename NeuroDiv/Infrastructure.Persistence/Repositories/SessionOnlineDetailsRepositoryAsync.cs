using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class SessionOnlineDetailsRepositoryAsync : GenericRepositoryAsync<SessionOnlineDetails>, ISessionOnlineDetailsRepositoryAsync
    {
        private readonly DbSet<SessionOnlineDetails> _SessionOnlineDetails;

        public SessionOnlineDetailsRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _SessionOnlineDetails = dbContext.Set<SessionOnlineDetails>();
        }

      
    }
}
