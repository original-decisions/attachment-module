using System;
using System.Collections.Generic;
using odec.Entity.DAL.Interop;

namespace odec.Attachment.DAL.Interop
{
    /// <summary>
    /// Репозиторий типов вложений
    /// </summary>
    public interface IAttachmentTypeRepository<TContext, in TKey, TAttachmentType, in TAttachmentTypeFilter, TExtension, TAttachment> :
        IEntityOperations<TKey, TAttachmentType>,
        IActivatableEntity<TKey,TAttachmentType>,
        IContextRepository<TContext> 
        where TKey : struct 
        where TAttachmentType : class
    {
        /// <summary>
        /// Определяет доступные типы расширения по типу вложения
        /// </summary>
        /// <param name="id">Идентификатор типа вложения</param>
        /// <returns>список расширений</returns>
        IEnumerable<TExtension> GetAttachmentTypeExtentions(TKey id);
        /// <summary>
        /// Выдает список вложений определенного типа
        /// </summary>
        /// <param name="id">Идентификатор типа вложения</param>
        /// <returns>список вложений</returns>

        IEnumerable<TAttachment> GetAttachmentTypeAttachments(TKey id);

        /// <summary>
        /// Определяет доступные типы вложений
        /// </summary>
        /// <returns>список типов вложений</returns>
        IEnumerable<TAttachmentType> Get(TAttachmentTypeFilter filter);

    }
}
