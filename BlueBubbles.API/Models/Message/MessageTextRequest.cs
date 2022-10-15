// This file is a part of the BlueBubbles .NET API library
// Copyright (c) 2022 Dylan Briedis <dylan@dylanbriedis.com>

using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BlueBubbles.API.Models.Message
{
    /// <summary>
    /// Defines the methods to use when sending a text message.
    /// </summary>
    public enum MessageTextMethod
    {
        /// <summary>
        /// Sends the message using AppleScript.
        /// </summary>
        [EnumMember(Value = "apple-script")]
        AppleScript,

        /// <summary>
        /// Sends the message using the Private API.
        /// </summary>
        [EnumMember(Value = "private-api")]
        PrivateAPI,
    }

    /// <summary>
    /// Request model for <c>POST /api/v1/message/text</c>.
    /// </summary>
    public sealed class MessageTextRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MessageTextRequest"/> class.
        /// </summary>
        /// <param name="chatGuid">The GUID for the chat you want this message to be sent to.</param>
        /// <param name="message">The text message to send.</param>
        public MessageTextRequest(string chatGuid, string message)
        {
            ChatGuid = chatGuid ?? throw new ArgumentNullException(nameof(chatGuid));
            Message = message ?? throw new ArgumentNullException(nameof(message));
        }

        /// <summary>
        /// Gets or sets the GUID for the chat this message to be sent to.
        /// </summary>
        public string ChatGuid { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a unique identifier for the message.
        /// This is to prevent duplicate messages from being sent.
        /// </summary>
        public string TempGuid { get; set; } = $"temp-{DateTime.Now.Ticks}";

        /// <summary>
        /// Gets or sets the text message to send.
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the method to use to send the message.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public MessageTextMethod Method { get; set; } = MessageTextMethod.AppleScript;

        /// <summary>
        /// Gets or sets the message's subject.
        /// </summary>
        /// <remarks>
        /// Setting this will force the method to be <see cref="MessageTextMethod.PrivateAPI"/>.
        /// </remarks>
        public string Subject { get; set; } = null;

        /// <summary>
        /// Gets or sets the effect ID.
        /// </summary>
        /// <remarks>
        /// Setting this will force the method to be <see cref="MessageTextMethod.PrivateAPI"/>.
        /// </remarks>
        public string EffectId { get; set; } = null;

        /// <summary>
        /// Gets or sets the message GUID to reply to.
        /// </summary>
        /// <remarks>
        /// Setting this will force the method to be <see cref="MessageTextMethod.PrivateAPI"/>.
        /// </remarks>
        public string SelectedMessageGuid { get; set; } = null;
    }
}
