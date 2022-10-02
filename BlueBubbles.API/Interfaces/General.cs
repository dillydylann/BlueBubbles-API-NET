// This file is a part of the BlueBubbles .NET API library
// Copyright (c) 2022 Dylan Briedis <dylan@dylanbriedis.com>

using System.Threading.Tasks;

namespace BlueBubbles.API.Interfaces
{
    /// <summary>
    /// Contains methods for the General API interface.
    /// </summary>
    public interface IGeneral
    {
        /// <summary>
        /// Sends a quick ping the Server to make sure it's online and responding to requests.
        /// </summary>
        /// <remarks>
        /// Requests with <c>GET /api/v1/ping</c>.
        /// </remarks>
        /// <returns>A response with a "pong" message.</returns>
        APIResponse<string> Ping();

        /// <inheritdoc cref="Ping"/>
        Task<APIResponse<string>> PingAsync();
    }

#pragma warning disable SA1600
    internal sealed class GeneralImpl : IGeneral
    {
        public const string PingUrlPath = "/api/v1/ping";

        private BlueBubblesClient client;
        public GeneralImpl(BlueBubblesClient c) => client = c;

        public APIResponse<string> Ping()
            => client.RequestGet<string>(PingUrlPath);
        public Task<APIResponse<string>> PingAsync()
            => client.RequestGetAsync<string>(PingUrlPath);
    }
#pragma warning restore SA1600
}
