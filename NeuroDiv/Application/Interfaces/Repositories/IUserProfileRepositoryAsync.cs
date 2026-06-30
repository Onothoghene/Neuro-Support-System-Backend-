using Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IUserProfileRepositoryAsync : IGenericRepositoryAsync<UserProfile>
    {
        
        Task<UserProfile> GetUserByOtpAsync(int otp);
        Task<UserProfile> GetUserByEmailAsync(string email);
        Task<UserProfile> GetUserByIdAsync(string userId);
        Task<UserProfile> GetUserProfileByIdAsync(string userId);
        Task<List<string>> GetUserIdsByEmail(List<string> emails);
        IQueryable<UserProfile> GetUserProfilesByIds(List<string> ids);
        IQueryable<UserProfile> GetUserProfilesByIds(IQueryable<string> ids);
        Task<UserProfile> GetUserProfilesByIdLite(string id);
        IQueryable<UserProfile> GetAllUsers();
    }
}
