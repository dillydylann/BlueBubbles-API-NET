// This file is a part of the BlueBubbles .NET API library
// Copyright (c) 2022 Dylan Briedis <dylan@dylanbriedis.com>

namespace BlueBubbles.API.Models.Chat
{
    /// <summary>
    /// Request model for <c>POST /api/v1/chat/query</c>.
    /// </summary>
    public sealed class ChatQueryRequest : BaseQueryRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChatQueryRequest"/> class.
        /// </summary>
        public ChatQueryRequest() { }

        /// <summary>
        /// Gets or sets the value to sort by.
        /// </summary>
        public string Sort { get; set; } = null;
    }
}
