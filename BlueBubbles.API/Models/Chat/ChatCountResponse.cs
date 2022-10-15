// This file is a part of the BlueBubbles .NET API library
// Copyright (c) 2022 Dylan Briedis <dylan@dylanbriedis.com>

#pragma warning disable SA1300 // For the iMessage property

namespace BlueBubbles.API.Models.Chat
{
    /// <summary>
    /// Response model for <c>GET /api/v1/chat/count</c>.
    /// </summary>
    public sealed class ChatCountResponse : BaseCountResponse
    {
        /// <summary>
        /// Gets the breakdown of each iMessage and SMS chats.
        /// </summary>
        public BreakdownData Breakdown { get; private set; }

        /// <summary>
        /// Defines the breakdown of each iMessage and SMS chats.
        /// </summary>
        public sealed class BreakdownData
        {
            /// <summary>
            /// Gets the total number of iMessage chats.
            /// </summary>
            public int iMessage { get; private set; }

            /// <summary>
            /// Gets the total number of SMS chats.
            /// </summary>
            public int SMS { get; private set; }
        }
    }
}
