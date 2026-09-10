using Domain.Common;
using Domain.Enums;
using System;

namespace Domain.Entities
{
    public class SessionNoShow : AuditableBaseEntity
    {
        public Guid SessionOccurrenceId { get; set; }
        public NoShowType NoShowType { get; set; }
        public string? Notes { get; set; }

        // Navigation
        public SessionOccurrence SessionOccurrence { get; set; }
    }
}
