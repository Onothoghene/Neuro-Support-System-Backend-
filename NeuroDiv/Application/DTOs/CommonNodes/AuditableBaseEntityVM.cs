using System;

namespace Application.DTOs.CommonNodes
{
    public class AuditableBaseEntityVM : BaseEntityVM
    {
        public string? CreatedBy { get; set; }
        public DateTime Created { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModified { get; set; }
        public DateTime? Deleted { get; set; }
        public string? DeletedBy { get; set; }
        public bool IsDeleted { get; set; }
    }

}
