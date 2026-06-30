using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Common
{
    public abstract class AuditableBaseEntity
    {
        public virtual Guid Id { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime Created { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModified { get; set; }
        public DateTime? Deleted { get; set; }
        public string? DeletedBy { get; set; }
        public bool IsDeleted { get; set; }

    }
}
