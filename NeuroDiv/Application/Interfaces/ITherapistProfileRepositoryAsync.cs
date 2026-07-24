using Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ITherapistProfileRepositoryAsync : IGenericRepositoryAsync<TherapistProfile>
    {
        Task<TherapistProfile?> GetByUserIdAsync(Guid userProfileId);
        Task<TherapistProfile?> GetByIdAsync(Guid id);
    }
}
