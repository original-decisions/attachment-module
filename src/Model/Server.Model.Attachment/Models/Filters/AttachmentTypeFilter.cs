using System.Collections.Generic;

namespace odec.Server.Model.Attachment.Filters
{
    public class AttachmentTypeFilter<TKey>
    {
        public bool IsOnlyActive { get; set; }

        public IEnumerable<TKey> Ids { get; set; }

        public string Name { get; set; }

    }
}
