using Domain.Common;
using Domain.Enums;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class ChildSessionRecord : AuditableBaseEntity
    {
        public ChildSessionRecord()
        {
            GoalProgressLogs = new HashSet<GoalProgressLog>();
        }

        public Guid SessionId { get; set; }
        public Guid ChildProfileId { get; set; }

        /// <summary>General observations for this child during this session.</summary>
        public string? GeneralNotes { get; set; }

        /// <summary>How engaged/responsive the child was this session.</summary>
        public ChildEngagement? Engagement { get; set; }

        // Navigation
        public Session Session { get; set; }
        public ChildProfile ChildProfile { get; set; }
        public ICollection<GoalProgressLog> GoalProgressLogs { get; set; }
    }
}
