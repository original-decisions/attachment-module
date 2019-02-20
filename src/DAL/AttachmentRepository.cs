using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using odec.Attachment.DAL.Interop;
using odec.Entity.DAL;
using odec.Framework.Logging;
using odec.Server.Model.Attachment.Filters;

namespace odec.Attachment.DAL
{
    public class AttachmentRepository: OrmEntityOperationsRepository<int, Server.Model.Attachment.Attachment, DbContext>, IAttachmentRepository<DbContext,int,Server.Model.Attachment.Attachment,AttachmentFilter<int?>>
    {
        public void SetConnection(string connection)
        {
            throw new NotImplementedException();
        }

        public void SetContext(DbContext db)
        {
            Db = db;
        }

        public virtual IEnumerable<Server.Model.Attachment.Attachment> Get(AttachmentFilter<int?> filter)
        {

            try
            {
                var  res =(from attachment in Db.Set<Server.Model.Attachment.Attachment>()
                    join attachmentType in Db.Set<Server.Model.Attachment.AttachmentType>() on attachment.AttachmentTypeId equals attachmentType.Id
                        where (!filter.AttachmentTypeId.HasValue || attachment.AttachmentTypeId == filter.AttachmentTypeId)
                        &&  (!filter.ExtensionId.HasValue || attachment.ExtensionId == filter.ExtensionId)
                        select attachment);


                return res;
            //        Db.Set<Server.Model.Attachment.Attachment>()
            //            .Include(it => it.AttachmentType)
            //            .Where(it => !filter.AttachmentTypeId.HasValue || it.AttachmentTypeId == filter.AttachmentTypeId)
            //            .Where(attach => !filter.ExtensionId.HasValue || attach.ExtensionId == filter.ExtensionId);
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex.Message,ex);
                throw;
            }
        }
    }
}
