using Domain.Common;
using System;

namespace Domain.Entities
{
    public class TherapistSpecialization : AuditableBaseEntity
    {
        public Guid TherapistProfileId { get; set; }

        /// <summary>
        /// The condition this therapist specializes in.
        /// e.g. "Autism", "ADHD", "Dyslexia", "Down Syndrome"
        /// </summary>
        public string Condition { get; set; }

        public string? Notes { get; set; }

        // Navigation
        public TherapistProfile TherapistProfile { get; set; }

    }
}
