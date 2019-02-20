using System.Collections.Generic;

namespace odec.Attachment.DAL.Interop
{
    public interface ISecureAttachmentRepository<TContext, in TKey, TAttachment, in TAttachmentFilter, TPermission, in TPermissionsFilter, TEntity> :
        IAttachmentRepository<TContext, TKey, TAttachment, TAttachmentFilter>
        where TAttachment : class
        where TKey : struct
    {
        /// <summary>
        /// ¬озвращает список разрешений дл€ конкретного вложени€ в разрезе роли и рц
        /// </summary>
        /// <returns>список разрешений</returns>
        IEnumerable<TPermission> GetAttachmentPermissions(TPermissionsFilter filter);

        void LinkAttachment(TEntity entity, TAttachment attachment, ICollection<TPermission> permissions = null);

        void RemoveAttachment(TEntity entity, TAttachment attachment, ICollection<TPermission> permissions = null);

    }
}