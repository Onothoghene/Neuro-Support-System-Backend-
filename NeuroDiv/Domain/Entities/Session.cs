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

        // ── Basic Info ────────────────────────────────────────────────────
        public string Title { get; set; }
        public SessionType Type { get; set; } = SessionType.Individual;
        public SessionStatus Status { get; set; } = SessionStatus.Scheduled;

        // ── Scheduling ────────────────────────────────────────────────────
        public DateTime ScheduledDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        /// <summary>
        /// Null = custom duration (derived from StartTime and EndTime).
        /// Populated = one of the fixed duration options.
        /// </summary>
        public Guid? SessionDurationId { get; set; }

        // ── Therapist ─────────────────────────────────────────────────────
        public Guid TherapistId { get; set; }   // UserProfile.Id

        // ── Ownership ─────────────────────────────────────────────────────
        /// <summary>Null for freelancer sessions.</summary>
        public Guid? OrganizationId { get; set; }

        // ── Recurring ─────────────────────────────────────────────────────
        public bool IsRecurring { get; set; } = false;

        /// <summary>
        /// Links recurring session occurrences together.
        /// All sessions in the same series share the same RecurringSeriesId.
        /// </summary>
        public Guid? RecurringSeriesId { get; set; }
        public RecurrencePattern? RecurrencePattern { get; set; }

        /// <summary>When the recurring series ends. Null = no end date.</summary>
        public DateTime? RecurrenceEndDate { get; set; }

        // ── Cancellation ──────────────────────────────────────────────────
        public CancellationReason? CancellationReason { get; set; }
        public string? CancellationNotes { get; set; }
        public DateTime? CancelledAt { get; set; }
        public Guid? CancelledBy { get; set; }

        // ── NoShow ────────────────────────────────────────────────────────
        public NoShowType? NoShowType { get; set; }
        public string? NoShowNotes { get; set; }

        // ── General Notes ─────────────────────────────────────────────────
        /// <summary>
        /// General session notes — applies to the whole session.
        /// Individual child notes live in ChildSessionRecord.
        /// </summary>
        public string? GeneralNotes { get; set; }

        // ── Navigation ────────────────────────────────────────────────────
        public UserProfile Therapist { get; set; }
        public Organizations? Organization { get; set; }
        public SessionDuration? SessionDuration { get; set; }
        public ICollection<ChildSessionRecord> ChildSessionRecords { get; set; }
    }
}
