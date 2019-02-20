using System.Collections.Generic;
using odec.Server.Model.Attachment.Filters;

namespace odec.Server.Model.Secure.Attachment.Specific.Filters
{
    public class SecuredAttachmentFilter<TKey,TRoleKey,TPermissionKey>: AttachmentFilter<TKey>
    {
        public IEnumerable<TRoleKey> RoleIds { get; set; }
        public IEnumerable<TPermissionKey> PermissionIds { get; set; }
    }
}
