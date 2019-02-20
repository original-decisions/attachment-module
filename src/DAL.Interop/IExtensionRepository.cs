using System.Collections.Generic;
using odec.Entity.DAL.Interop;

namespace odec.Attachment.DAL.Interop
{
    public interface IExtensionRepository<TContext, in TKey,TExtension>:IContextRepository<TContext>,IEntityOperations<TKey,TExtension>,IActivatableEntity<TKey,TExtension> 
        where TExtension : class 
        where TKey : struct
    {
        IEnumerable<TExtension> Get();
    }
}