using Domain.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Domain.Entities
{
    public class Organizations : AuditableBaseEntity
    {
        public Organizations()
        {
            OrganizationUsers = new HashSet<OrganizationUsers>();
            //Activities = new HashSet<Activity>();
        }

        public required string Name { get; set; }
        public string Type { get; set; }
        public string? LogoUrl { get; set; }
        public string? Website { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? Domain { get; set; }
        public string? Description { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }

        public ICollection<OrganizationUsers> OrganizationUsers { get; set; }
      //  public ICollection<Activity> Activities { get; set; }
    }
}
