// This file is a part of the BlueBubbles .NET API library
// Copyright (c) 2022 Dylan Briedis <dylan@dylanbriedis.com>

using System.Threading.Tasks;
using BlueBubbles.API.Models;
using BlueBubbles.API.Models.Handle;

namespace BlueBubbles.API.Interfaces
{
    /// <summary>
    /// Contains methods for the Handle API interface.
    /// </summary>
    public interface IHandle
    {
        /// <summary>
        /// Get the total number of handles on the server.
        /// </summary>
        /// <remarks>
        /// Requests with <c>GET /api/v1/handle/count</c>.
        /// </remarks>
        /// <returns>A response containing the total number of handles.</returns>
        APIResponse<HandleCountResponse> Count();

        /// <inheritdoc cref="Count"/>
        Task<APIResponse<HandleCountResponse>> CountAsync();

        /// <summary>
        /// Queries handles from the server.
        /// </summary>
        /// <remarks>
        /// Requests with <c>POST /api/v1/handle/query</c>.
        /// </remarks>
        /// <param name="request">The parameters for this request.</param>
        /// <returns>A response with an array of handles.</returns>
        APIResponse<HandleEntity[], QueryMetadata> Query(HandleQueryRequest request);

        /// <inheritdoc cref="Query"/>
        Task<APIResponse<HandleEntity[], QueryMetadata>> QueryAsync(HandleQueryRequest request);

        /// <summary>
        /// Fetches a handle's database information by address.
        /// </summary>
        /// <remarks>
        /// Requests with <c>GET /api/v1/handle/:guid</c>.
        /// </remarks>
        /// <param name="guid">The handle's address.</param>
        /// <returns>A response containing the handle entity.</returns>
        APIResponse<HandleEntity> Find(string guid);

        /// <inheritdoc cref="Find"/>
        Task<APIResponse<HandleEntity>> FindAsync(string guid);
    }

#pragma warning disable SA1600
    internal sealed class HandleImpl : IHandle
    {
        public const string CountUrlPath = "/api/v1/handle/count";
        public const string QueryUrlPath = "/api/v1/handle/query";
        public const string FindUrlPath = "/api/v1/handle/{0}";

        private BlueBubblesClient client;
        public HandleImpl(BlueBubblesClient c) => client = c;

        public APIResponse<HandleCountResponse> Count()
            => client.RequestGet<HandleCountResponse>(CountUrlPath);
        public Task<APIResponse<HandleCountResponse>> CountAsync()
            => client.RequestGetAsync<HandleCountResponse>(CountUrlPath);

        public APIResponse<HandleEntity[], QueryMetadata> Query(HandleQueryRequest request)
            => client.RequestPost<HandleEntity[], QueryMetadata, HandleQueryRequest>(QueryUrlPath, request);
        public Task<APIResponse<HandleEntity[], QueryMetadata>> QueryAsync(HandleQueryRequest request)
            => client.RequestPostAsync<HandleEntity[], QueryMetadata, HandleQueryRequest>(QueryUrlPath, request);

        public APIResponse<HandleEntity> Find(string guid)
            => client.RequestGet<HandleEntity>(Utils.StringFormatUriEncode(FindUrlPath, guid));
        public Task<APIResponse<HandleEntity>> FindAsync(string guid)
            => client.RequestGetAsync<HandleEntity>(Utils.StringFormatUriEncode(FindUrlPath, guid));
    }
#pragma warning restore SA1600
}
