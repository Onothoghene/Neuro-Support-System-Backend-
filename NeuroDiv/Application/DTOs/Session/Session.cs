using Application.DTOs.ChildSessionRecord;
using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;

namespace Application.DTOs.Session
{
    public class CreateSessionRequest
    {
        public required string Title { get; set; }
        public SessionType Type { get; set; } = SessionType.Individual;
        public DateTime ScheduledDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public Guid? SessionDurationId { get; set; }
        public Guid? OrganizationId { get; set; }

        /// <summary>
        /// Child(ren) to add to this session.
        /// One for Individual, multiple for Group.
        /// </summary>
        public List<Guid> ChildProfileIds { get; set; } = new();

        // ── Recurring 
        public bool IsRecurring { get; set; } = false;
        public RecurrencePattern? RecurrencePattern { get; set; }
        public DateTime? RecurrenceEndDate { get; set; }
    }

    public class UpdateSessionRequest
    {
        public string? Title { get; set; }
        public DateTime? ScheduledDate { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public Guid? SessionDurationId { get; set; }
        public string? GeneralNotes { get; set; }
    }

    public class CancelSessionRequest
    {
        public CancelType CancelType { get; set; }
        public CancellationReason Reason { get; set; }
        public string? Notes { get; set; }
    }

    public class MarkNoShowRequest
    {
        public NoShowType NoShowType { get; set; }
        public string? Notes { get; set; }
    }

    public class SessionVM
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public DateTime ScheduledDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string? DurationLabel { get; set; }
        public string TherapistFirstName { get; set; }
        public string TherapistLastName { get; set; }
        public bool IsRecurring { get; set; }
        public string? RecurrencePattern { get; set; }
        public DateTime? RecurrenceEndDate { get; set; }
        public string? GeneralNotes { get; set; }
        public string? CancellationReason { get; set; }
        public string? CancellationNotes { get; set; }
        public string? NoShowType { get; set; }
        public string? NoShowNotes { get; set; }
        public List<ChildSessionRecordVM> ChildRecords { get; set; } = new();
    }

    public class SessionSummaryVM
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public DateTime ScheduledDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string? DurationLabel { get; set; }
        public int ChildCount { get; set; }
        public bool IsRecurring { get; set; }
    }
}
