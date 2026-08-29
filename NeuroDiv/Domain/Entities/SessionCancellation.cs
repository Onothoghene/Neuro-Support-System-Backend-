using Domain.Common;
using Domain.Enums;
using System;

namespace Domain.Entities
{
    public class SessionCancellation : AuditableBaseEntity
    {
        public Guid SessionId { get; set; }
        public CancellationReason Reason { get; set; }
        public string? Notes { get; set; }
        public DateTime CancelledAt { get; set; }

        // Navigation
        public Session Session { get; set; }
    }
}
