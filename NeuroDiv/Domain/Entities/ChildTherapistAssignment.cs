using Domain.Common;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class ChildTherapistAssignment : AuditableBaseEntity
    {
        public Guid ChildProfileId { get; set; }
        public Guid TherapistId { get; set; }   // UserProfile.Id

        public AssignmentRole Role { get; set; } = AssignmentRole.Primary;

        public DateTime StartDate { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Null = still actively assigned.
        /// Filled only when therapist is removed from this child.
        /// </summary>
        public DateTime? EndDate { get; set; }

        public bool IsActive => EndDate == null;

        // Navigation
        public ChildProfile ChildProfile { get; set; }
        public UserProfile Therapist { get; set; }
    }

    public enum AssignmentRole
    {
        Primary = 0,
        Secondary = 1,
        Specialist = 2
    }

}
