// This file is a part of the BlueBubbles .NET API library
// Copyright (c) 2022 Dylan Briedis <dylan@dylanbriedis.com>

#pragma warning disable SA1300 // For the iMessage value

using System;
using System.Collections.Generic;
using System.Linq;
using BlueBubbles.API.Models.Message;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BlueBubbles.API.Models.Chat
{
    /// <summary>
    /// Defines the services to use when creating a chat.
    /// </summary>
    public enum ChatServiceType
    {
        /// <summary>
        /// Sends the message using iMessage.
        /// </summary>
        iMessage,

        /// <summary>
        /// Sends the message using the Short Message Service.
        /// </summary>
        SMS,
    }

    /// <summary>
    /// Request model for <c>POST /api/v1/chat/new</c>.
    /// </summary>
    public sealed class ChatCreateRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChatCreateRequest"/> class.
        /// </summary>
        /// <param name="addresses">The list of participants for the chat.</param>
        /// <param name="message">The message to send in the new chat.</param>
        public ChatCreateRequest(List<string> addresses, string message)
        {
            Addresses = addresses ?? throw new ArgumentNullException(nameof(addresses));
            Message = message ?? throw new ArgumentNullException(nameof(message));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChatCreateRequest"/> class.
        /// </summary>
        /// <param name="addresses">The list of participants for the chat.</param>
        /// <param name="message">The message to send in the new chat.</param>
        public ChatCreateRequest(IEnumerable<string> addresses, string message)
        {
            Addresses = addresses?.ToList() ?? throw new ArgumentNullException(nameof(addresses));
            Message = message ?? throw new ArgumentNullException(nameof(message));
        }

        /// <summary>
        /// Gets or sets the list of participants for the chat.
        /// </summary>
        public List<string> Addresses { get; set; } = new List<string>();

        /// <summary>
        /// Gets or sets the message to send in the new chat.
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the method to use to send the message.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public MessageTextMethod Method { get; set; } = MessageTextMethod.AppleScript;

        /// <summary>
        /// Gets or sets the service to use to send the message.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public ChatServiceType Service { get; set; } = ChatServiceType.iMessage;

        /// <summary>
        /// Gets or sets a unique identifier for the message.
        /// This is to prevent duplicate messages from being sent.
        /// </summary>
        public string TempGuid { get; set; } = $"temp-{DateTime.Now.Ticks}";
    }
}
