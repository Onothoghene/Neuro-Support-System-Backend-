using Application.DTOs.CommonNodes;
using System;

namespace Application.DTOs.OrganizationUsers
{
    public class OrganizationUsersVM : AuditableBaseEntityVM
    {
        public Guid UserId { get; set; }
        public Guid OrganizationId { get; set; }
    }

    public class UserOrganizationVM
    {
        public Guid OrganizationId { get; set; }
        public string OrganizationName { get; set; }
        public string Role { get; set; }
        public bool IsActive { get; set; }
    }
}
