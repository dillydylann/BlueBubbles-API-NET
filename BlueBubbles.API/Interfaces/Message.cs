// This file is a part of the BlueBubbles .NET API library
// Copyright (c) 2022 Dylan Briedis <dylan@dylanbriedis.com>

using System.Threading.Tasks;
using BlueBubbles.API.Models;
using BlueBubbles.API.Models.Message;

namespace BlueBubbles.API.Interfaces
{
    /// <summary>
    /// Contains methods for the Message API interface.
    /// </summary>
    public interface IMessage
    {
        /// <summary>
        /// Send a text message via iMessage.
        /// </summary>
        /// <remarks>
        /// Requests with <c>POST /api/v1/message/text</c>.
        /// </remarks>
        /// <param name="request">The parameters for this request.</param>
        /// <returns>A response containing the newly created message entity.</returns>
        APIResponse<MessageEntity> SendText(MessageTextRequest request);

        /// <inheritdoc cref="SendText"/>
        Task<APIResponse<MessageEntity>> SendTextAsync(MessageTextRequest request);

        /// <summary>
        /// Send an attachment via iMessage.
        /// </summary>
        /// <remarks>
        /// Requests with <c>POST /api/v1/message/attachment</c>.
        /// </remarks>
        /// <param name="request">The parameters for this request.</param>
        /// <returns>A response containing the newly created message entity.</returns>
        APIResponse<MessageEntity> SendAttachment(MessageAttachmentRequest request);

        /// <inheritdoc cref="SendAttachment"/>
        Task<APIResponse<MessageEntity>> SendAttachmentAsync(MessageAttachmentRequest request);

        /// <summary>
        /// Reacts to a message.
        /// </summary>
        /// <remarks>
        /// Requests with <c>POST /api/v1/message/react</c>.
        /// <para/>
        /// Only works with the Private API enabled.
        /// </remarks>
        /// <param name="request">The parameters for this request.</param>
        /// <returns>A response containing the reaction message entity.</returns>
        [PrivateAPI]
        APIResponse<MessageEntity> React(MessageReactRequest request);

        /// <inheritdoc cref="React"/>
        [PrivateAPI]
        Task<APIResponse<MessageEntity>> ReactAsync(MessageReactRequest request);

        /// <summary>
        /// Gets the total number of messages on the server.
        /// </summary>
        /// <remarks>
        /// Requests with <c>GET /api/v1/message/count</c>.
        /// </remarks>
        /// <param name="after">Fetches messages before a specified date, represented in seconds since EPOCH.</param>
        /// <param name="before">Fetches messages after a specified date, represented in seconds since EPOCH.</param>
        /// <returns>A response containing the total number of messages.</returns>
        APIResponse<MessageCountResponse> Count(long? after = null, long? before = null);

        /// <inheritdoc cref="Count"/>
        Task<APIResponse<MessageCountResponse>> CountAsync(long? after = null, long? before = null);

        /// <summary>
        /// </summary>
        /// <remarks>
        /// Requests with <c>GET /api/v1/message/count/updated</c>.
        /// </remarks>
        /// <param name="after">Fetches messages before a specified date, represented in seconds since EPOCH.</param>
        /// <param name="before">Fetches messages after a specified date, represented in seconds since EPOCH.</param>
        /// <returns>A response containing the total number of messages.</returns>
        APIResponse<MessageCountResponse> UpdatedCount(long after, long? before = null);

        /// <inheritdoc cref="UpdatedCount"/>
        Task<APIResponse<MessageCountResponse>> UpdatedCountAsync(long after, long? before = null);

        /// <summary>
        /// Gets the total number of messages sent by the client on the server.
        /// </summary>
        /// <remarks>
        /// Requests with <c>GET /api/v1/message/count/me</c>.
        /// </remarks>
        /// <returns>A response containing the total number of messages.</returns>
        APIResponse<MessageCountResponse> SentCount();

        /// <inheritdoc cref="SentCount"/>
        Task<APIResponse<MessageCountResponse>> SentCountAsync();

        /// <summary>
        /// Queries messages from the server.
        /// </summary>
        /// <remarks>
        /// Requests with <c>POST /api/v1/message/query</c>.
        /// </remarks>
        /// <param name="request">The parameters for this request.</param>
        /// <returns>A response with an array of messages.</returns>
        APIResponse<MessageEntity[], QueryMetadata> Query(MessageQueryRequest request);

        /// <inheritdoc cref="Query"/>
        Task<APIResponse<MessageEntity[], QueryMetadata>> QueryAsync(MessageQueryRequest request);

        /// <summary>
        /// Fetches a messages's database information by GUID.
        /// </summary>
        /// <remarks>
        /// Requests with <c>GET /api/v1/message/:<paramref name="guid"/></c>.
        /// </remarks>
        /// <param name="guid">The message GUID.</param>
        /// <param name="with">The types of values to return.</param>
        /// <returns>A response containing the message entity.</returns>
        APIResponse<MessageEntity> Find(string guid, params string[] with);

        /// <inheritdoc cref="Find"/>
        Task<APIResponse<MessageEntity>> FindAsync(string guid, params string[] with);
    }

#pragma warning disable SA1600
    internal sealed class MessageImpl : IMessage
    {
        public const string TextUrlPath = "/api/v1/message/text";
        public const string AttachmentUrlPath = "/api/v1/message/attachment";
        public const string ReactUrlPath = "/api/v1/message/react";
        public const string CountUrlPath = "/api/v1/message/count";
        public const string UpdatedCountUrlPath = "/api/v1/message/count/updated";
        public const string SentCountUrlPath = "/api/v1/message/count/me";
        public const string QueryUrlPath = "/api/v1/message/query";
        public const string FindUrlPath = "/api/v1/message/{0}";

        private BlueBubblesClient client;
        public MessageImpl(BlueBubblesClient c) => client = c;

        public APIResponse<MessageEntity> SendText(MessageTextRequest request)
            => client.RequestPost<MessageEntity, MessageTextRequest>(TextUrlPath, request);
        public Task<APIResponse<MessageEntity>> SendTextAsync(MessageTextRequest request)
            => client.RequestPostAsync<MessageEntity, MessageTextRequest>(TextUrlPath, request);

        public APIResponse<MessageEntity> SendAttachment(MessageAttachmentRequest request)
            => client.RequestPost<MessageEntity, MessageAttachmentRequest>(AttachmentUrlPath, request, "multipart/form-data");
        public Task<APIResponse<MessageEntity>> SendAttachmentAsync(MessageAttachmentRequest request)
            => client.RequestPostAsync<MessageEntity, MessageAttachmentRequest>(AttachmentUrlPath, request, "multipart/form-data");

        public APIResponse<MessageEntity> React(MessageReactRequest request)
            => client.RequestPost<MessageEntity, MessageReactRequest>(ReactUrlPath, request);
        public Task<APIResponse<MessageEntity>> ReactAsync(MessageReactRequest request)
            => client.RequestPostAsync<MessageEntity, MessageReactRequest>(ReactUrlPath, request);

        public APIResponse<MessageCountResponse> Count(long? after, long? before)
            => client.RequestGet<MessageCountResponse>(CountUrlPath, Utils.BuildUrlQuery(new { before, after }));
        public Task<APIResponse<MessageCountResponse>> CountAsync(long? after, long? before)
            => client.RequestGetAsync<MessageCountResponse>(CountUrlPath, Utils.BuildUrlQuery(new { before, after }));

        public APIResponse<MessageCountResponse> UpdatedCount(long after, long? before)
            => client.RequestGet<MessageCountResponse>(UpdatedCountUrlPath, Utils.BuildUrlQuery(new { before, after }));
        public Task<APIResponse<MessageCountResponse>> UpdatedCountAsync(long after, long? before)
            => client.RequestGetAsync<MessageCountResponse>(UpdatedCountUrlPath, Utils.BuildUrlQuery(new { before, after }));

        public APIResponse<MessageCountResponse> SentCount()
            => client.RequestGet<MessageCountResponse>(SentCountUrlPath);
        public Task<APIResponse<MessageCountResponse>> SentCountAsync()
            => client.RequestGetAsync<MessageCountResponse>(SentCountUrlPath);

        public APIResponse<MessageEntity[], QueryMetadata> Query(MessageQueryRequest request)
            => client.RequestPost<MessageEntity[], QueryMetadata, MessageQueryRequest>(QueryUrlPath, request);
        public Task<APIResponse<MessageEntity[], QueryMetadata>> QueryAsync(MessageQueryRequest request)
            => client.RequestPostAsync<MessageEntity[], QueryMetadata, MessageQueryRequest>(QueryUrlPath, request);

        public APIResponse<MessageEntity> Find(string guid, params string[] with)
            => client.RequestGet<MessageEntity>(Utils.StringFormatUriEncode(FindUrlPath, guid), Utils.BuildUrlQuery(new { with = string.Join(",", with) }));
        public Task<APIResponse<MessageEntity>> FindAsync(string guid, params string[] with)
            => client.RequestGetAsync<MessageEntity>(Utils.StringFormatUriEncode(FindUrlPath, guid), Utils.BuildUrlQuery(new { with = string.Join(",", with) }));
    }
#pragma warning restore SA1600
}
