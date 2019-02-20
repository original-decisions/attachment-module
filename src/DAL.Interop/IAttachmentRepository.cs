using System.Collections.Generic;
using odec.Entity.DAL.Interop;

namespace odec.Attachment.DAL.Interop
{
    public interface IAttachmentRepository<TContext, in TKey,TAttachment, in TAttachmentFilter> :
        IContextRepository<TContext>,
        IEntityOperations<TKey,TAttachment>,
        IActivatableEntity<TKey,TAttachment> 
        where TKey : struct 
        where TAttachment : class
    {
        IEnumerable<TAttachment> Get(TAttachmentFilter filter);
        
    }
}
