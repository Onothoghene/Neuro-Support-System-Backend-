using Domain.Common;
using System;

namespace Domain.Entities
{
    public class OrganizationUsers : AuditableBaseEntity
    {
        public Guid UserId { get; set; }
        public Guid OrganizationId { get; set; }

        public DateTime JoinedAt { get; set; }

        public UserProfile User { get; set; }
        public Organizations Organizations { get; set; }
    }
}
