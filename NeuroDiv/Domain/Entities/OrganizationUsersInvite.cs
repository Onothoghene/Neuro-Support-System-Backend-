using Domain.Common;
using System;

namespace Domain.Entities
{
    public class OrganizationUsersInvite : AuditableBaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; } // The target email
        public string Token { get; set; } // Unique link token
        public bool IsAccepted { get; set; } = false;
        public DateTime ExpiryDate { get; set; }
        public Guid OrganizationId { get; set; }

        public Organizations Organization { get; set; }
    }
}
