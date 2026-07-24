using Domain.Common;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class UserProfile : AuditableBaseEntity
    {
        public UserProfile()
        {
            OrganizationUserRoles = new HashSet<OrganizationUserRoles>();
            EmailChangeRequest = new HashSet<EmailChangeRequest>();
        }

        public string? GenderId { get; set; }
        public required string FirstName { get; set; }
        public string? OtherName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public int VerificationCode { get; set; }
        public bool? IsLoggedIn { get; set; }
        public DateTime? LastDateLoggedIn { get; set; }

        public ICollection<OrganizationUserRoles> OrganizationUserRoles { get; set; }
        public ICollection<EmailChangeRequest> EmailChangeRequest { get; set; }
        //public TherapistProfile? TherapistProfile { get; set; }
    }
}
