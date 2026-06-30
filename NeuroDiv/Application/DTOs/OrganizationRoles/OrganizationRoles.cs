using Application.DTOs.CommonNodes;
using System;

namespace Application.DTOs.OrganizationRoles
{
    public class OrganizationRolesVM : AuditableBaseEntityVM
    {
        public Guid UserId { get; set; }
        public Guid OrganizationId { get; set; }
    }

}
