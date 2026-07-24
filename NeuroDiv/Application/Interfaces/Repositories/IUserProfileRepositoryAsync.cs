using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IUserProfileRepositoryAsync : IGenericRepositoryAsync<UserProfile>
    {
        
        Task<UserProfile> GetUserByOtpAsync(int otp);
        Task<UserProfile> GetUserByEmailAsync(string email);
        Task<UserProfile?> GetUserByIdAsync(Guid userId);
        Task<List<Guid>> GetUserIdsByEmail(List<string> emails);
        IQueryable<UserProfile> GetUserProfilesByIds(List<Guid> ids);
        IQueryable<UserProfile> GetUserProfilesByIds(IQueryable<Guid> ids);
        IQueryable<UserProfile> GetAllUsers();
    }
}
