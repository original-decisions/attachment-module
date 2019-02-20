using System;
using System.Collections.Generic;
using odec.Entity.DAL.Interop;

namespace odec.Attachment.DAL.Interop
{
    /// <summary>
    /// ����������� ����� ��������
    /// </summary>
    public interface IAttachmentTypeRepository<TContext, in TKey, TAttachmentType, in TAttachmentTypeFilter, TExtension, TAttachment> :
        IEntityOperations<TKey, TAttachmentType>,
        IActivatableEntity<TKey,TAttachmentType>,
        IContextRepository<TContext> 
        where TKey : struct 
        where TAttachmentType : class
    {
        /// <summary>
        /// ���������� ��������� ���� ���������� �� ���� ��������
        /// </summary>
        /// <param name="id">������������� ���� ��������</param>
        /// <returns>������ ����������</returns>
        IEnumerable<TExtension> GetAttachmentTypeExtentions(TKey id);
        /// <summary>
        /// ������ ������ �������� ������������� ����
        /// </summary>
        /// <param name="id">������������� ���� ��������</param>
        /// <returns>������ ��������</returns>

        IEnumerable<TAttachment> GetAttachmentTypeAttachments(TKey id);

        /// <summary>
        /// ���������� ��������� ���� ��������
        /// </summary>
        /// <returns>������ ����� ��������</returns>
        IEnumerable<TAttachmentType> Get(TAttachmentTypeFilter filter);

    }
}
