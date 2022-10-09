// This file is a part of the BlueBubbles .NET API library
// Copyright (c) 2022 Dylan Briedis <dylan@dylanbriedis.com>

namespace BlueBubbles.API.Models.Server
{
    /// <summary>
    /// Defines the totals for media entities per chat.
    /// </summary>
    public sealed class ServerStatMediaByChatTotals
    {
        /// <summary>
        /// Gets the chat GUID.
        /// </summary>
        public string ChatGuid { get; private set; }

        /// <summary>
        /// Gets the chat group name.
        /// </summary>
        public string GroupName { get; private set; }

        /// <summary>
        /// Gets the total number of media entities on the chat.
        /// </summary>
        public ServerStatMediaTotals Totals { get; private set; }
    }
}
