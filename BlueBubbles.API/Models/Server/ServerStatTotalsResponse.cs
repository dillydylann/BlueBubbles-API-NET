// This file is a part of the BlueBubbles .NET API library
// Copyright (c) 2022 Dylan Briedis <dylan@dylanbriedis.com>

using Newtonsoft.Json;

namespace BlueBubbles.API.Models.Server
{
    /// <summary>
    /// Response model for <c>GET /api/v1/server/statistics/totals</c>.
    /// </summary>
    public sealed class ServerStatTotalsResponse
    {
        /// <summary>
        /// Gets the total number of handles.
        /// </summary>
        public int Handles { get; private set; }

        /// <summary>
        /// Gets the total number of messages.
        /// </summary>
        public int Messages { get; private set; }

        /// <summary>
        /// Gets the total number of chats.
        /// </summary>
        public int Chats { get; private set; }

        /// <summary>
        /// Gets the total number of attachments.
        /// </summary>
        public int Attachments { get; private set; }

        /// <summary>
        /// Gets the total number of iMessage entities.
        /// </summary>
        [JsonIgnore]
        public int Total => Handles + Messages + Chats + Attachments;
    }
}
