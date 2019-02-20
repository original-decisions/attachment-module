using odec.Framework.Generic;

namespace odec.Server.Model.Attachment.Filters
{
    public class AttachmentFilter<TKey>:FilterBase
    {
        public TKey AttachmentTypeId { get; set; }
        public TKey ExtensionId { get; set; }
        public bool IsOnlyActive { get; set; }


    }
}
