using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using odec.Attachment.DAL.Interop;
using odec.Entity.DAL;
using odec.Framework.Logging;
using odec.Server.Model.Attachment.Extended;

namespace odec.Attachment.DAL
{
    public class ExtensionRepository: OrmEntityOperationsRepository<int, Extension, DbContext>,IExtensionRepository<DbContext, int,Extension>
    {
        public ExtensionRepository()
        {
            
        }

        public ExtensionRepository(DbContext db)
        {
            Db = db;
        }
        public IEnumerable<Extension> Get()
        {
            try
            {
                return Db.Set<Extension>();
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex.Message,ex);
                throw;
            }
        }

        public void SetConnection(string connection)
        {
            throw new NotImplementedException();
        }

        public void SetContext(DbContext db)
        {
            Db = db;
        }
    }
}
