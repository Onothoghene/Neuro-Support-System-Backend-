using Application.DTOs.CommonNodes;
using System;

namespace Application.DTOs.OrganizationUsers
{
    public class OrganizationUsersVM : AuditableBaseEntityVM
    {
        public Guid UserId { get; set; }
        public Guid OrganizationId { get; set; }
    }

}
