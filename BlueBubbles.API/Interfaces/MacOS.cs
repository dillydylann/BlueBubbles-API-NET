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
        APIResponse<object> Lock();

        /// <inheritdoc cref="Lock"/>
        Task<APIResponse<object>> LockAsync();

        /// <summary>
        /// Restarts the Messages app on the server.
        /// </summary>
        /// <remarks>
        /// Requests with <c>POST /api/v1/mac/imessage/restart</c>.
        /// </remarks>
        /// <returns>A response with no data.</returns>
        APIResponse<object> RestartMessagesApp();

        /// <inheritdoc cref="RestartMessagesApp"/>
        Task<APIResponse<object>> RestartMessagesAppAsync();
    }

#pragma warning disable SA1600
    internal sealed class MacOSImpl : IMacOS
    {
        public const string LockUrlPath = "/api/v1/mac/lock";
        public const string RestartMessagesAppUrlPath = "/api/v1/mac/imessage/restart";

        private BlueBubblesClient client;
        public MacOSImpl(BlueBubblesClient c) => client = c;

        public APIResponse<object> Lock()
            => client.RequestPost<object>(LockUrlPath);
        public Task<APIResponse<object>> LockAsync()
            => client.RequestPostAsync<object>(LockUrlPath);

        public APIResponse<object> RestartMessagesApp()
            => client.RequestPost<object>(RestartMessagesAppUrlPath);
        public Task<APIResponse<object>> RestartMessagesAppAsync()
            => client.RequestPostAsync<object>(RestartMessagesAppUrlPath);
    }
#pragma warning restore SA1600
}
