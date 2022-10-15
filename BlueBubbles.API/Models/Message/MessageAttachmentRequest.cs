// This file is a part of the BlueBubbles .NET API library
// Copyright (c) 2022 Dylan Briedis <dylan@dylanbriedis.com>

using System;
using System.IO;

namespace BlueBubbles.API.Models.Message
{
    /// <summary>
    /// Request model for <c>POST /api/v1/message/attachment</c>.
    /// </summary>
    public sealed class MessageAttachmentRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MessageAttachmentRequest"/> class.
        /// </summary>
        /// <param name="chatGuid">The GUID for the chat you want this message to be sent to.</param>
        /// <param name="name">The attachment file name.</param>
        /// <param name="attachment">The attachment content to send.</param>
        public MessageAttachmentRequest(string chatGuid, string name, Stream attachment)
        {
            ChatGuid = chatGuid ?? throw new ArgumentNullException(nameof(chatGuid));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Attachment = attachment ?? throw new ArgumentNullException(nameof(attachment));
        }

        /// <summary>
        /// Gets or sets the GUID for the chat you want this message to be sent to.
        /// </summary>
        public string ChatGuid { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a unique identifier for the message.
        /// This is to prevent duplicate messages from being sent.
        /// </summary>
        public string TempGuid { get; set; } = $"temp-{DateTime.Now.Ticks}";

        /// <summary>
        /// Gets or sets the attachment file name.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the attachment content to send.
        /// </summary>
        public Stream Attachment { get; set; } = Stream.Null;
    }
}
