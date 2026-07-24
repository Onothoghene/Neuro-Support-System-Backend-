using Application.Enums;
using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.Xml;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class UserProfileRepositoryAsync : GenericRepositoryAsync<UserProfile>, IUserProfileRepositoryAsync
    {
        private readonly DbSet<UserProfile> _userProfile;

        public UserProfileRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _userProfile = dbContext.Set<UserProfile>();
        }

        public Task<UserProfile> GetUserByOtpAsync(int otp)
        {
            return _userProfile.Where(x => x.VerificationCode == otp).FirstOrDefaultAsync();
        }

        public Task<UserProfile> GetUserByEmailAsync(string email)
        {
            return _userProfile.Where(x => x.Email == email).FirstOrDefaultAsync();
        }

        public Task<UserProfile?> GetUserByIdAsync(Guid userId)
        {
            //Guid userIdGuid = Guid.Parse(userId);
            return _userProfile.Where(x => x.Id == userId).FirstOrDefaultAsync();
        }

        public async Task<List<Guid>> GetUserIdsByEmail(List<string> emails)
        {
            var userIds = await _userProfile.Where(x => emails.Contains(x.Email))
                                      .Select(x => x.Id)
                                      .ToListAsync();
            return userIds;

        }

        public IQueryable<UserProfile> GetUserProfilesByIds(List<Guid> ids)
        {
          //  var userGuids = ids.Select(id => Guid.Parse(id)).ToList();
            var users = _userProfile.Where(x => ids.Contains(x.Id));

            return users;
        }

        public IQueryable<UserProfile> GetUserProfilesByIds(IQueryable<Guid> ids)
        {
            //var guidIds = ids.Select(Guid.Parse).ToList();

        //    var guidIds = ids
        //.Select(id => Guid.TryParse(id, out var guid) ? guid : (Guid?)null)
        //.Where(g => g.HasValue)
        //.Select(g => g.Value)
        //.ToList();

            var users = _userProfile.Where(x => ids.Contains(x.Id));

            return users;
        }

        public IQueryable<UserProfile> GetAllUsers()
        {
            return _userProfile.AsQueryable();
        }

    }
}
