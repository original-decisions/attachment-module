using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using odec.Server.Model.Attachment;
using odec.Server.Model.User;

namespace odec.Server.Model.Secure.Attachment
{
    public class AttachmentTypePermission
    {
     //   [Key, Column(Order = 3)]
        public int PermissionId { get; set; }
        public Permission Permission { get; set; }
   //     [Key,Column(Order = 0)]
        public int RoleId { get; set; }
        public Role Role { get; set; }
    //    [Key, Column(Order = 1)]
        public int AttachmentTypeId { get; set; }
        public AttachmentType AttachmentType { get; set; }
    }
}
