// This file is a part of the BlueBubbles .NET API library
// Copyright (c) 2022 Dylan Briedis <dylan@dylanbriedis.com>

using Newtonsoft.Json;

namespace BlueBubbles.API.Models.Server
{
    /// <summary>
    /// Defines the totals for media entities.
    /// </summary>
    public class ServerStatMediaTotals
    {
        /// <summary>
        /// Gets the total number of images.
        /// </summary>
        public int Images { get; private set; }

        /// <summary>
        /// Gets the total number of videos.
        /// </summary>
        public int Videos { get; private set; }

        /// <summary>
        /// Gets the total number of location attachments.
        /// </summary>
        public int Locations { get; private set; }

        /// <summary>
        /// Gets the total number of media entities.
        /// </summary>
        [JsonIgnore]
        public int Total => Images + Videos + Locations;
    }

    /// <summary>
    /// Response model for <c>GET /api/v1/server/statistics/media</c>.
    /// </summary>
    public sealed class ServerStatMediaResponse : ServerStatMediaTotals
    {
    }
}
