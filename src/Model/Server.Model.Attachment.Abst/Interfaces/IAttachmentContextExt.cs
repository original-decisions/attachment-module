using Microsoft.EntityFrameworkCore;

namespace odec.Server.Model.Attachment.Abst.Interfaces
{
    public interface IAttachmentContextExt<TAttachment, TAttachmentType, TExtension, TAttachmentTypeExtention> : IAttachmentContext<TAttachment, TAttachmentType>
        where TExtension : class
        where TAttachment : class
        where TAttachmentType : class 
        where TAttachmentTypeExtention : class
    {
        /// <summary>
        /// Extentions table|view
        /// </summary>
        DbSet<TExtension> Extensions { get; set; }
        DbSet<TAttachmentTypeExtention> AttachmentTypeExtentions { get; set; } 
    }
}