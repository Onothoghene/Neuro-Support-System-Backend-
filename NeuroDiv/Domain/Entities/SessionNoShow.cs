using Domain.Common;
using Domain.Enums;
using System;

namespace Domain.Entities
{
    public class SessionNoShow : AuditableBaseEntity
    {
        public Guid SessionId { get; set; }
        public NoShowType NoShowType { get; set; }
        public string? Notes { get; set; }

        // Navigation
        public Session Session { get; set; }
    }
}
