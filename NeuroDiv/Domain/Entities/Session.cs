using Domain.Common;
using Domain.Enums;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Session : AuditableBaseEntity
    {
        public Session()
        {
            ChildSessionRecords = new HashSet<ChildSessionRecord>();
        }

        public string Title { get; set; }
        public SessionType Type { get; set; } = SessionType.Individual;
        public SessionStatus Status { get; set; } = SessionStatus.Scheduled;

        //Scheduling
        public DateTime ScheduledDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public Guid? SessionDurationId { get; set; }

        //Ownership
        public Guid TherapistId { get; set; }
        public Guid? OrganizationId { get; set; }

        //General Notes 
        public string? GeneralNotes { get; set; }

        //Navigation 
        public UserProfile Therapist { get; set; }
        public Organizations? Organization { get; set; }
        public SessionDuration? SessionDuration { get; set; }
        public SessionCancellation? Cancellation { get; set; }
        public SessionNoShow? NoShow { get; set; }
        public SessionRecurrence? Recurrence { get; set; }
        public ICollection<ChildSessionRecord> ChildSessionRecords { get; set; }
    }
}
