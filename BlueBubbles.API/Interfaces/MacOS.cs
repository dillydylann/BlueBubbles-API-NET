// This file is a part of the BlueBubbles .NET API library
// Copyright (c) 2022 Dylan Briedis <dylan@dylanbriedis.com>

using System.Threading.Tasks;

namespace BlueBubbles.API.Interfaces
{
    /// <summary>
    /// Contains methods for the MacOS API interface.
    /// </summary>
    public interface IMacOS
    {
        /// <summary>
        /// Locks the server device.
        /// </summary>
        /// <remarks>
        /// Requests with <c>POST /api/v1/mac/lock</c>.
        /// </remarks>
        /// <returns>A response with no data.</returns>
        APIResponse Lock();

        /// <inheritdoc cref="Lock"/>
        Task<APIResponse> LockAsync();

        /// <summary>
        /// Restarts the Messages app on the server.
        /// </summary>
        /// <remarks>
        /// Requests with <c>POST /api/v1/mac/imessage/restart</c>.
        /// </remarks>
        /// <returns>A response with no data.</returns>
        APIResponse RestartMessagesApp();

        /// <inheritdoc cref="RestartMessagesApp"/>
        Task<APIResponse> RestartMessagesAppAsync();
    }

#pragma warning disable SA1600
    internal sealed class MacOSImpl : IMacOS
    {
        public const string LockUrlPath = "/api/v1/mac/lock";
        public const string RestartMessagesAppUrlPath = "/api/v1/mac/imessage/restart";

        private BlueBubblesClient client;
        public MacOSImpl(BlueBubblesClient c) => client = c;

        public APIResponse Lock()
            => client.RequestPost(LockUrlPath);
        public Task<APIResponse> LockAsync()
            => client.RequestPostAsync(LockUrlPath);

        public APIResponse RestartMessagesApp()
            => client.RequestPost(RestartMessagesAppUrlPath);
        public Task<APIResponse> RestartMessagesAppAsync()
            => client.RequestPostAsync(RestartMessagesAppUrlPath);
    }
#pragma warning restore SA1600
}
