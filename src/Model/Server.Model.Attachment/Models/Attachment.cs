using odec.Framework.Generic;
using odec.Server.Model.Attachment.Extended;

namespace odec.Server.Model.Attachment
{
    /// <summary>
    /// серверный объект - вложение
    /// </summary>
    public class Attachment:Glossary<int>
    {
        /// <summary>
        /// идентификатор типа вложения
        /// </summary>
        public int AttachmentTypeId { get; set; }

        /// <summary>
        /// серверный объект -тип вложения
        /// </summary>
        public AttachmentType AttachmentType { get; set; }

        public Extension Extension { get; set; }

        public int ExtensionId { get; set; }
        /// <summary>
        /// содержание вложения
        /// </summary>
        public byte[] Content { get; set; }

        public bool IsShared { get; set; }

        public string PublicUri { get; set; }
    }
}
