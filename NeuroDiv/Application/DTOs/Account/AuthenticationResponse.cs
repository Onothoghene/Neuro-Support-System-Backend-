using Application.DTOs.OrganizationUsers;
using System;
using System.Collections.Generic;

namespace Application.DTOs.Account
{
    public class AuthenticationResponse
    {
        //public string IdentityId { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }
        public bool IsVerified { get; set; }
        public string JWToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime TokenExpires { get; set; }
        public DateTime RefreshTokenExpiration { get; set; }
        public bool HasPreference { get; set; }

        public List<UserOrganizationVM> Organizations { get; set; } = new();
    }
}
