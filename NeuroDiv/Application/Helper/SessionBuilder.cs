using Application.DTOs.GoalProgressLog;
using Application.Features.Session.Command;
using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Helper
{
    public static class SessionBuilder
    {
        public static Session BuildSession(CreateSessionCommand command, Guid therapistId, Guid? seriesId,
                                            List<ChildSessionRecord> childRecords, DateTime? overrideDate = null)
        {
            return new Session
            {
                Title = command.Title,
                Type = command.Type,
                Status = SessionStatus.Scheduled,
                ScheduledDate = overrideDate ?? command.ScheduledDate,
                StartTime = command.StartTime,
                EndTime = command.EndTime,
                SessionDurationId = command.SessionDurationId,
                TherapistId = therapistId,
                OrganizationId = command.OrganizationId,
                IsRecurring = command.IsRecurring,
                RecurringSeriesId = seriesId,
                RecurrencePattern = command.RecurrencePattern,
                RecurrenceEndDate = command.RecurrenceEndDate,
                ChildSessionRecords = childRecords,
            };
        }

        public static List<Session> GenerateRecurringSessions(CreateSessionCommand command, Guid therapistId,
                                                               Guid seriesId, List<ChildSessionRecord> childRecords)
        {
            var sessions = new List<Session>();
            var current = command.ScheduledDate;
            var endDate = command.RecurrenceEndDate ?? command.ScheduledDate.AddMonths(3);

            while (current <= endDate)
            {
                // Each session gets its own child record copies
                var sessionChildRecords = childRecords.Select(r => new ChildSessionRecord
                {
                    ChildProfileId = r.ChildProfileId,
                    Created = DateTime.UtcNow,
                }).ToList();

                sessions.Add(BuildSession(command, therapistId, seriesId, sessionChildRecords, current));

                current = command.RecurrencePattern switch
                {
                    RecurrencePattern.Daily => current.AddDays(1),
                    RecurrencePattern.Weekly => current.AddDays(7),
                    RecurrencePattern.Biweekly => current.AddDays(14),
                    RecurrencePattern.Monthly => current.AddMonths(1),
                    _ => endDate.AddDays(1) // break the loop
                };
            }

            return sessions;
        }

        public static void ApplyCancellation(Session session, CancelSessionCommand command, Guid loggedInUserId)
        {
            session.Status = SessionStatus.Cancelled;
            session.CancellationReason = command.Reason;
            session.CancellationNotes = command.Notes;
            session.CancelledAt = DateTime.UtcNow;
            session.CancelledBy = loggedInUserId;
            session.LastModified = DateTime.UtcNow;
            session.LastModifiedBy = loggedInUserId.ToString();
        }

        public static List<GoalProgressLog> BuildGoalProgressLogs(List<GoalProgressLogRequest> requests)
        {
            return requests.Select(r => new GoalProgressLog
            {
                TherapyGoalId = r.TherapyGoalId,
                ProgressNote = r.ProgressNote,
                ProgressRating = r.ProgressRating,
                StatusUpdate = r.StatusUpdate,
            }).ToList();
        }
    }
}
