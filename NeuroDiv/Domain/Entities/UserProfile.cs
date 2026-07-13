using Domain.Common;
using System;

namespace Domain.Entities
{
    public class UserProfile : AuditableBaseEntity
    {
        public UserProfile()
        {
        }

        public string? GenderId { get; set; }
        public string FirstName { get; set; }
        public string OtherName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public int VerificationCode { get; set; }
        public bool? IsLoggedIn { get; set; }
        public DateTime? LastDateLoggedIn { get; set; }


    }
}
