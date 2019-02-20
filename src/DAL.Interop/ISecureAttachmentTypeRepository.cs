using System.Collections.Generic;

namespace odec.Attachment.DAL.Interop
{
    public interface ISecureAttachmentTypeRepository<TContext, in TKey, TAttachmentType, in TAttachmentTypeFilter, TExtension, TAttachment, TPermission, in TAttachmentTypePermissionFilter> : 
        IAttachmentTypeRepository<TContext, TKey, TAttachmentType, TAttachmentTypeFilter, TExtension, TAttachment> 
        where TKey : struct 
        where TAttachmentType : class
    {
        /// <summary>
        /// ���������� ����� �� �������� ������������� ����.
        /// </summary>
        /// <returns>������ ����</returns>
        IEnumerable<TPermission> GetAttachmentTypePermissions(TAttachmentTypePermissionFilter filter);
    }
}