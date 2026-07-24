using Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IEmailChangeRequestRepositoryAsync : IGenericRepositoryAsync<EmailChangeRequest>
    {
        /// <summary>Gets the latest pending (not used, not expired) request for a user.</summary>
        Task<EmailChangeRequest?> GetPendingByUserProfileIdAsync(Guid userId);

    }
}
