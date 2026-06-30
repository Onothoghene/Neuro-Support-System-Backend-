using Application.DTOs.CommonNodes;
using System;

namespace Application.DTOs.OrganizationUserRoles
{
    public class OrganizationUserRolesVM : AuditableBaseEntityVM
    {
        public Guid UserId { get; set; }
        public Guid OrganizationId { get; set; }
        public Guid OrganizationRoleId { get; set; }
    }

}
