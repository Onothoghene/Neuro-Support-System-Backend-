using Application.DTOs.ChildSessionRecord;
using Application.DTOs.CommonNodes;
using Application.DTOs.Session;
using System;
using System.Collections.Generic;

namespace Application.DTOs.SessionOccurrence
{
    public class SessionOccurrenceVM : BaseEntityVM
    {
        public Guid SessionClassId { get; set; }
        public string SessionTitle { get; set; }
        public string TherapistFirstName { get; set; }
        public string TherapistLastName { get; set; }
        public string ChildFirstName { get; set; }
        public string ChildLastName { get; set; }
        public DateTime ScheduledDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string Status { get; set; }
        public DateTime? ActualStartTime { get; set; }
        public DateTime? ActualEndTime { get; set; }
        public string? GeneralNotes { get; set; }
        public string Mode { get; set; }
        public OnlineSessionDetailsVM? OnlineDetails { get; set; }
        public SessionCancellationVM? Cancellation { get; set; }
        public SessionNoShowVM? NoShow { get; set; }
        public List<ChildSessionRecordVM> ChildRecords { get; set; } = new();
    }

    public class SessionOccurrenceSummaryVM : BaseEntityVM
    {
        public DateTime ScheduledDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string Status { get; set; }
    }

    public class SessionCancellationVM : BaseEntityVM
    {
        public string Reason { get; set; }
        public string? Notes { get; set; }
        public DateTime CancelledAt { get; set; }
    }

    public class SessionNoShowVM : BaseEntityVM
    {
        public string NoShowType { get; set; }
        public string? Notes { get; set; }
    }
}
