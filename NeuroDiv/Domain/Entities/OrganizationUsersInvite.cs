using Domain.Common;
using System;

namespace Domain.Entities
{
    public class OrganizationUsersInvite : AuditableBaseEntity
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; } // The target email
        public string Token { get; set; } // Unique link token
        public bool IsAccepted { get; set; } = false;
        public DateTime ExpiryDate { get; set; }
        public Guid OrganizationId { get; set; }

        public Organizations Organization { get; set; }
    }
}
