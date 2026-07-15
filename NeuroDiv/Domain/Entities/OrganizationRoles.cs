using Domain.Common;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class OrganizationRoles : AuditableBaseEntity
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public Guid? OrganizationId { get; set; }

        public UserProfile User { get; set; }
        public Organizations Organizations { get; set; }
        public ICollection<OrganizationUserRoles> UserRoles { get; set; }
    }
}
