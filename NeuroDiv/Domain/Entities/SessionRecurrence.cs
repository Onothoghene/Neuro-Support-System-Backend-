using Domain.Common;
using Domain.Enums;
using System;

namespace Domain.Entities
{
    public class SessionRecurrence : AuditableBaseEntity
    {
        /// <summary>
        /// All sessions in the same recurring series share this Id.
        /// Used to fetch and cancel entire series.
        /// </summary>
        public Guid SeriesId { get; set; }

        public RecurrencePattern Pattern { get; set; }
        public DateTime? EndDate { get; set; }

        // Navigation
        public Session Session { get; set; }
    }
}
