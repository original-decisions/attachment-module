using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Attach = odec.Server.Model.Attachment.Attachment;
namespace odec.Server.Model.Secure.Attachment
{
    public class AttachmentPermission
    {
        [Key,Column(Order = 2)]
        public int PermissionId { get; set; }
        public Permission Permission { get; set; }
        [Key,Column(Order = 0)]
        public int UserId { get; set; }
        public User.User User { get; set; }
        [Key,Column(Order = 1)]
        public int AttachmentId { get; set; }
        public Attach Attachment { get; set; } 
        public bool IsOwner { get; set; }
    }
}
