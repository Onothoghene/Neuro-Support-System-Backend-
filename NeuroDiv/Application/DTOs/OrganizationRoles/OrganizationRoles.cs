using Application.DTOs.CommonNodes;
using System;

namespace Application.DTOs.OrganizationRoles
{
    public class OrganizationRolesVM : AuditableBaseEntityVM
    {
        public Guid? OrganizationId { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool IsDefault { get; set; }
    }

}
