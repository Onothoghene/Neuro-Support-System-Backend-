using Application.DTOs.GoalProgressLog;
using Domain.Enums;
using System;
using System.Collections.Generic;

namespace Application.DTOs.ChildSessionRecord
{
    public class AddOrUpdateChildSessionRecordRequest
    {
        public Guid ChildProfileId { get; set; }
        public string? GeneralNotes { get; set; }
        public ChildEngagement? Engagement { get; set; }
        public List<GoalProgressLogRequest> GoalProgressLogs { get; set; } = new();
    }

    public class ChildSessionRecordVM
    {
        public Guid Id { get; set; }
        public Guid ChildProfileId { get; set; }
        public string ChildFirstName { get; set; }
        public string ChildLastName { get; set; }
        public string? GeneralNotes { get; set; }
        public string? Engagement { get; set; }
        public List<GoalProgressLogVM> GoalProgressLogs { get; set; } = new();
    }
}
