// This file is a part of the BlueBubbles .NET API library
// Copyright (c) 2022 Dylan Briedis <dylan@dylanbriedis.com>

using System.Collections.Generic;
using BlueBubbles.API.Models.Message;

namespace BlueBubbles.API.Models.Attachment
{
    /// <summary>
    /// Represents an attachment.
    /// </summary>
    /// <seealso href="https://github.com/BlueBubblesApp/bluebubbles-server/blob/9572e21180c95587b45dd3cbe4f7568a0bf47661/packages/server/src/server/types.ts#L87"/>
    public class AttachmentEntity : BaseEntity
    {
        /// <summary>
        /// Gets the attachment's GUID.
        /// </summary>
        public string Guid { get; private set; }

        /// <summary>
        /// Gets the messages associated with the attachment.
        /// </summary>
        public MessageEntity[] Messages { get; private set; }

        /// <summary>
        /// Gets the data of the attachment in a Base64 format.
        /// </summary>
        public string Data { get; private set; }

        /// <summary>
        /// Gets BlurHash of the attachment.
        /// </summary>
        public string Blurhash { get; private set; }

        /// <summary>
        /// Gets the width of the attachment.
        /// </summary>
        public int Width { get; private set; }

        /// <summary>
        /// Gets the height of the attachment.
        /// </summary>
        public int Height { get; private set; }

        /// <summary>
        /// Gets the attachment's UTI (Uniform Type Identifier).
        /// </summary>
        public string Uti { get; private set; }

        /// <summary>
        /// Gets the attachment's MIME type.
        /// </summary>
        public string MimeType { get; private set; }

        /// <summary>
        /// </summary>
        public int TransferState { get; private set; }

        /// <summary>
        /// </summary>
        public bool IsOutgoing { get; private set; }

        /// <summary>
        /// Gets the attachment's file name for saving.
        /// </summary>
        public string TransferName { get; private set; }

        /// <summary>
        /// Gets the total size in bytes of the attachment.
        /// </summary>
        public int TotalBytes { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the attachment is a sticker.
        /// </summary>
        public bool IsSticker { get; private set; }

        /// <summary>
        /// </summary>
        public bool HideAttachment { get; private set; }

        /// <summary>
        /// Gets the attachment's original GUID.
        /// </summary>
        public string OriginalGuid { get; private set; }

        /// <summary>
        /// Gets the metadata associated with the attachment.
        /// </summary>
        public Dictionary<string, object> Metadata { get; private set; }
    }
}
