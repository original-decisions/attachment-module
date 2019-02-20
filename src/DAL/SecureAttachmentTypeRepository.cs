using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using odec.Attachment.DAL.Interop;
using odec.Framework.Logging;
using odec.Server.Model.Attachment;
using odec.Server.Model.Attachment.Extended;
using odec.Server.Model.Attachment.Filters;
using odec.Server.Model.Secure.Attachment;
using odec.Server.Model.Secure.Attachment.Specific.Filters;

namespace odec.Attachment.DAL
{
    public class SecureAttachmentTypeRepository:AttachmentTypeRepository,ISecureAttachmentTypeRepository<DbContext,int,AttachmentType,AttachmentTypeFilter<int>,Extension,Server.Model.Attachment.Attachment,Permission,AttachmentTypePermissionFilter<int?>>
    {
        public SecureAttachmentTypeRepository() : base() { }
        public SecureAttachmentTypeRepository(DbContext db) :base(db){ }

        public IEnumerable<Permission> GetAttachmentTypePermissions(AttachmentTypePermissionFilter<int?> filter)
        {
            try
            {
                if (!filter.AttachmentTypeId.HasValue)
                    throw new ArgumentException("No AttachmentTypeId provided");

                return (from attachmentTypePermission in Db.Set<AttachmentTypePermission>()
                    join attachmentType in Db.Set<AttachmentType>() on attachmentTypePermission.AttachmentTypeId equals
                        attachmentType.Id
                    join permission in Db.Set<Permission>() on attachmentTypePermission.PermissionId equals
                        permission.Id
                    where filter.AttachmentTypeId == attachmentTypePermission.AttachmentTypeId &&
                    (!filter.IsOnlyActive || (filter.IsOnlyActive && attachmentType.IsActive))
                          &&
                          (!filter.RoleId.HasValue ||
                           (filter.RoleId.HasValue && filter.RoleId == attachmentTypePermission.RoleId))
                    select permission).Distinct();
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex.Message,ex);
                throw;
            }
        }
    }
}
