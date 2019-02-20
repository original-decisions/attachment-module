using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace odec.Server.Model.Attachment.Extended
{
    public class AttachmentTypeExtension
    {
      //  [Key,Column(Order = 0)]
        public int AttachmentTypeId { get; set; }
        public AttachmentType AttachmentType { get; set; }
    //    [Key,Column(Order = 1)]
        public int ExtensionId { get; set; }
        public Extension Extension { get; set; }
    }
}
