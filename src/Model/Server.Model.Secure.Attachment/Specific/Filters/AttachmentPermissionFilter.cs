using odec.Framework.Generic;

namespace odec.Server.Model.Secure.Attachment.Specific.Filters
{
    public class AttachmentPermissionFilter<TAttachmentKey, TRoleKey, TUserKey>:FilterBase
    {
        public TAttachmentKey AttachmentId { get; set; }
        public bool IsOnlyActive { get; set; }
        public TRoleKey RoleId { get; set; }
        public TUserKey UserId { get; set; }
    }
}
