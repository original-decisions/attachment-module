namespace odec.Server.Model.Attachment.Filters
{
    public class AttachmentExtensionsFilter<TKey>
    {
        public TKey AttachmentId { get; set; }
        public bool IsOnlyActive { get; set; }
    }
}
