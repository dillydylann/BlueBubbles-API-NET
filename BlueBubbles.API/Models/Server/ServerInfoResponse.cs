// This file is a part of the BlueBubbles .NET API library
// Copyright (c) 2022 Dylan Briedis <dylan@dylanbriedis.com>

using Newtonsoft.Json;

namespace BlueBubbles.API.Models.Server
{
    /// <summary>
    /// Response model for <c>GET /api/v1/server/info</c>.
    /// </summary>
    public sealed class ServerInfoResponse
    {
        /// <summary>
        /// Gets the server's operating system version.
        /// </summary>
        [JsonProperty("os_version")]
        public string OSVersion { get; private set; }

        /// <summary>
        /// Gets the BlueBubbles server version.
        /// </summary>
        [JsonProperty("server_version")]
        public string ServerVersion { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the Private API is enabled.
        /// </summary>
        [JsonProperty("private_api")]
        public bool PrivateAPIEnabled { get; private set; }

        /// <summary>
        /// Gets the proxy service used for the server.
        /// </summary>
        [JsonProperty("proxy_service")]
        public string ProxyService { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the Private API helper is connected.
        /// </summary>
        [JsonProperty("helper_connected")]
        public bool HelperConnected { get; private set; }

        /// <summary>
        /// Gets the iCloud email address that's tied to the server.
        /// </summary>
        [JsonProperty("detected_icloud")]
        public string DetectediCloudEmail { get; private set; }
    }
}
