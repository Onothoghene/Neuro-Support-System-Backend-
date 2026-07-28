using Application.DTOs.CommonNodes;
using System;

namespace Application.DTOs.ChildParent
{
    public class ChildParentVM : AuditableBaseEntityVM
    {
        public Guid ChildId { get; set; }
        public Guid ParentId { get; set; }
    }
    

}
