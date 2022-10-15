// This file is a part of the BlueBubbles .NET API library
// Copyright (c) 2022 Dylan Briedis <dylan@dylanbriedis.com>

using System;

namespace BlueBubbles.API.Models.Chat
{
    /// <summary>
    /// Request model for <c>POST /api/v1/chat/:guid/participant/add</c>,
    /// and <c>POST /api/v1/chat/:guid/participant/remove</c>.
    /// </summary>
    public sealed class ChatParticipantRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChatParticipantRequest"/> class.
        /// </summary>
        /// <param name="address">The participant address to add or remove.</param>
        public ChatParticipantRequest(string address)
        {
            Address = address ?? throw new ArgumentNullException(nameof(address));
        }

        /// <summary>
        /// Gets or sets the participant address to add or remove.
        /// </summary>
        public string Address { get; set; } = string.Empty;
    }
}
