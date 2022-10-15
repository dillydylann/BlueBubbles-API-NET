// This file is a part of the BlueBubbles .NET API library
// Copyright (c) 2022 Dylan Briedis <dylan@dylanbriedis.com>

using System;

namespace BlueBubbles.API.Models.Chat
{
    /// <summary>
    /// Request model for <c>PUT /api/v1/chat/:guid</c>.
    /// </summary>
    public sealed class ChatUpdateRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChatUpdateRequest"/> class.
        /// </summary>
        /// <param name="displayName">The new name for the group chat.</param>
        public ChatUpdateRequest(string displayName)
        {
            DisplayName = displayName ?? throw new ArgumentNullException(nameof(displayName));
        }

        /// <summary>
        /// Gets or sets the new name for the group chat.
        /// </summary>
        public string DisplayName { get; set; } = string.Empty;
    }
}
