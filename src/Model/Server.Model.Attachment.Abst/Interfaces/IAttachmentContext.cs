using Microsoft.EntityFrameworkCore;

namespace odec.Server.Model.Attachment.Abst.Interfaces
{
    /// <summary>
    /// Прокси объект контекста вложения
    /// </summary>
    /// <typeparam name="TAttachment">тип вложения</typeparam>
    /// <typeparam name="TAttachmentType">тип типа вложения</typeparam>
    public interface IAttachmentContext<TAttachment, TAttachmentType> where TAttachment : class where TAttachmentType : class
    {
        /// <summary>
        /// таблица связи вложений
        /// </summary>
        DbSet<TAttachment> Attachments { get; set; }

        /// <summary>
        /// таблица связи типов вложений
        /// </summary>
        DbSet<TAttachmentType> AttachmentTypes { get; set; }
    }
}
