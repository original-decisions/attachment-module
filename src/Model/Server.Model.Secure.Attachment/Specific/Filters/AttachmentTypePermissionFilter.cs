using odec.Framework.Generic;

namespace odec.Server.Model.Secure.Attachment.Specific.Filters
{
    public class AttachmentTypePermissionFilter<TKey>: FilterBase
    {
        public bool IsOnlyActive { get; set; }
        public TKey RoleId { get; set; }
        public TKey AttachmentTypeId { get; set; }
    }
}
