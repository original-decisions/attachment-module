using Microsoft.EntityFrameworkCore;

namespace odec.Server.Model.Attachment.Abst.Interfaces
{
    public interface ISecureAttachmentContext
        <TAttachment, TAttachmentType, TExtention, TAttachmentTypeExtention, TAttachmentTypePermission, TPermission, TAttachmentPermission> :
        IAttachmentContextExt<TAttachment, TAttachmentType, TExtention, TAttachmentTypeExtention> 
        where TAttachment : class 
        where TAttachmentType : class 
        where TExtention : class 
        where TAttachmentTypeExtention : class 
        where TAttachmentTypePermission : class 
        where TPermission : class 
        where TAttachmentPermission : class
    {
         
        DbSet<TPermission> Permissions { get; set; }
        DbSet<TAttachmentTypePermission>  AttachmentTypePermissions { get; set; }
        DbSet<TAttachmentPermission> AttachmentPermissions { get; set; }
    }
}