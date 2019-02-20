using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using odec.Attachment.DAL.Interop;
using odec.Entity.DAL;
using odec.Framework.Logging;
using odec.Server.Model.Attachment;
using odec.Server.Model.Attachment.Extended;
using odec.Server.Model.Attachment.Filters;

namespace odec.Attachment.DAL
{
    public class AttachmentTypeRepository : OrmEntityOperationsRepository<int, AttachmentType, DbContext>, IAttachmentTypeRepository<DbContext, int, AttachmentType, AttachmentTypeFilter<int>, Extension, Server.Model.Attachment.Attachment>
    {
        public AttachmentTypeRepository() { }
        public AttachmentTypeRepository(DbContext db)
        {
            Db = db;
        }
        public void SetConnection(string connection)
        {
            throw new NotImplementedException();
        }
        public void SetContext(DbContext db)
        {
            Db = db;
        }
        public virtual IEnumerable<AttachmentType> Get(AttachmentTypeFilter<int> filter)
        {
            try
            {
                var hasName = !string.IsNullOrEmpty(filter.Name);
                var hasIds = filter.Ids != null && filter.Ids.Any();
                var res =(from attachmentType in Db.Set<AttachmentType>()
                    where (!filter.IsOnlyActive || (filter.IsOnlyActive && attachmentType.IsActive))
                    && (!hasName || (hasName && filter.Name == attachmentType.Name))
                    && (!hasIds || (hasIds && filter.Ids.Contains(attachmentType.Id)))
                    select attachmentType).ToList();
                return res;

            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex.Message,ex);
                throw;
            }
        }
        public virtual IEnumerable<Extension> GetAttachmentTypeExtentions(int id)
        {
            try
            {
                return
                    Db.Set<AttachmentTypeExtension>()
                        .Where(it => it.AttachmentTypeId == id)
                        .Include(it => it.Extension)
                        .Select(it => it.Extension);
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex.Message,ex);
                throw;
            }
        }
        public virtual IEnumerable<Server.Model.Attachment.Attachment> GetAttachmentTypeAttachments(int id)
        {
            try
            {
                return Db.Set<Server.Model.Attachment.Attachment>().Where(it => it.AttachmentTypeId == id);
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex.Message,ex);
                throw;
            }
        }


    }
}
