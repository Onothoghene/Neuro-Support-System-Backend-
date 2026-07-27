using Domain.Common;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class ParentProfile : AuditableBaseEntity
    {
        public ParentProfile()
        {
            Children = new HashSet<ChildParent>();
        }

        public string FirstName { get; set; }
        public string? OtherName { get; set; }
        public string LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }

        /// <summary>Mother, Father, Guardian, Grandparent, Caregiver etc.</summary>
        public string Relationship { get; set; }

        public bool IsPrimaryContact { get; set; } = false;
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// For future login capability — links to UserProfile when parent
        /// gets their own account. Null until then.
        /// </summary>
        public Guid? UserProfileId { get; set; }

        // Navigation
        public UserProfile? UserProfile { get; set; }
        public ICollection<ChildParent> Children { get; set; }
    }
}
