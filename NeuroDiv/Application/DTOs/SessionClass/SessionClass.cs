using Application.DTOs.ChildSessionRecord;
using Application.DTOs.CommonNodes;
using Application.DTOs.SessionOccurrence;
using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;

namespace Application.DTOs.Session
{
    public class CreateSessionClassRequest
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public Guid ChildProfileId { get; set; }
        public Guid? OrganizationId { get; set; }
        public SessionMode Mode { get; set; } = SessionMode.Physical;
        public bool IsRecurring { get; set; } = false;

        /// <summary>
        /// Required for both recurring AND one-off sessions.
        /// For one-off: just set StartTime, EndTime, SessionDurationId.
        /// DaysOfWeek only needed for recurring.
        /// </summary>
        public SessionScheduleRequest Schedule { get; set; }

        /// <summary>Required if Mode = Online or Hybrid.</summary>
        public OnlineSessionDetailsRequest? OnlineDetails { get; set; }

        /// <summary>Required if IsRecurring = true.</summary>
        public RecurrenceRequest? Recurrence { get; set; }
    }

    public class SessionScheduleRequest
    {
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public Guid? SessionDurationId { get; set; }
    }

    public class RecurrenceRequest
    {
        public List<DayOfWeek> DaysOfWeek { get; set; } = new();
        public DateTime? RecurrenceEndDate { get; set; }
    }

    public class OnlineSessionDetailsRequest
    {
        public OnlinePlatform Platform { get; set; }
        public string? MeetingLink { get; set; }
        public string? MeetingId { get; set; }
        public string? MeetingPassword { get; set; }
        public string? JoiningInstructions { get; set; }
    }

    public class UpdateSessionClassRequest
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public SessionMode? Mode { get; set; }
        public bool? IsActive { get; set; }
        public OnlineSessionDetailsRequest? OnlineDetails { get; set; }
    }

    public class SessionClassVM : BaseEntityVM
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public string TherapistFirstName { get; set; }
        public string TherapistLastName { get; set; }
        public string ChildFirstName { get; set; }
        public string ChildLastName { get; set; }
        public string Mode { get; set; }
        public bool IsRecurring { get; set; }
        public bool IsActive { get; set; }
        public RecurrenceRuleVM? RecurrenceRule { get; set; }
        public OnlineSessionDetailsVM? OnlineDetails { get; set; }
        public List<SessionOccurrenceSummaryVM> UpcomingOccurrences { get; set; } = new();
    }

    public class RecurrenceRuleVM : BaseEntityVM
    {
        public List<string> DaysOfWeek { get; set; } = new();
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string? DurationLabel { get; set; }
        public DateTime? RecurrenceEndDate { get; set; }
    }

    public class OnlineSessionDetailsVM : BaseEntityVM
    {
        public string Platform { get; set; }
        public string? MeetingLink { get; set; }
        public string? MeetingId { get; set; }
        public string? MeetingPassword { get; set; }
        public string? JoiningInstructions { get; set; }
    }

}
