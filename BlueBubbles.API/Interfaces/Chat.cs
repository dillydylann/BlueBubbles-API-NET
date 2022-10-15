// This file is a part of the BlueBubbles .NET API library
// Copyright (c) 2022 Dylan Briedis <dylan@dylanbriedis.com>

using System.IO;
using System.Threading.Tasks;
using BlueBubbles.API.Models;
using BlueBubbles.API.Models.Chat;
using BlueBubbles.API.Models.Message;

namespace BlueBubbles.API.Interfaces
{
    /// <summary>
    /// Contains methods for the Chat API interface.
    /// </summary>
    public interface IChat
    {
        /// <summary>
        /// Create a new iMessage chat.
        /// </summary>
        /// <remarks>
        /// Requests with <c>POST /api/v1/chat/new</c>.
        /// </remarks>
        /// <param name="request">The parameters for this request.</param>
        /// <returns>A response containing the newly created chat entity.</returns>
        APIResponse<ChatEntity> Create(ChatCreateRequest request);

        /// <inheritdoc cref="Query"/>
        Task<APIResponse<ChatEntity>> CreateAsync(ChatCreateRequest request);

        /// <summary>
        /// Gets the total number of chats on the server.
        /// </summary>
        /// <remarks>
        /// Requests with <c>GET /api/v1/chat/count</c>.
        /// </remarks>
        /// <returns>A response containing the total number of chats.</returns>
        APIResponse<ChatCountResponse> Count();

        /// <inheritdoc cref="Count"/>
        Task<APIResponse<ChatCountResponse>> CountAsync();

        /// <summary>
        /// Queries for chats from the server.
        /// </summary>
        /// <remarks>
        /// Requests with <c>POST /api/v1/chat/query</c>.
        /// </remarks>
        /// <param name="request">The parameters for this request.</param>
        /// <returns>A response with an array of chats.</returns>
        APIResponse<ChatEntity[], QueryMetadata> Query(ChatQueryRequest request);

        /// <inheritdoc cref="Query"/>
        Task<APIResponse<ChatEntity[], QueryMetadata>> QueryAsync(ChatQueryRequest request);

        /// <summary>
        /// Fetches messages associated with a specified chat GUID.
        /// </summary>
        /// <remarks>
        /// Requests with <c>GET /api/v1/chat/:guid/message</c>.
        /// </remarks>
        /// <param name="guid">The chat GUID.</param>
        /// <param name="before">Fetches messages after a specified date, represented in seconds since EPOCH.</param>
        /// <param name="after">Fetches messages before a specified date, represented in seconds since EPOCH.</param>
        /// <param name="limit">The number representing the limit for the database query.</param>
        /// <param name="offset">The number representing the offset for the database query.</param>
        /// <param name="sort">The value to sort by.</param>
        /// <param name="with">The types of values to return.</param>
        /// <returns>A response with an array of messages.</returns>
        APIResponse<MessageEntity[]> GetMessages(string guid, long? before = null, long? after = null, int? limit = null, int? offset = null, string sort = null, params string[] with);

        /// <inheritdoc cref="GetMessages"/>
        Task<APIResponse<MessageEntity[]>> GetMessagesAsync(string guid, long? before = null, long? after = null, int? limit = null, int? offset = null, string sort = null, params string[] with);

        /// <summary>
        /// Marks a chat as read.
        /// </summary>
        /// <remarks>
        /// Requests with <c>POST /api/v1/chat/:guid/read</c>.
        /// <para/>
        /// Only works with the Private API enabled.
        /// </remarks>
        /// <param name="guid">The chat GUID.</param>
        /// <returns>A response with no data.</returns>
        [PrivateAPI]
        APIResponse MarkRead(string guid);

        /// <inheritdoc cref="MarkRead"/>
        [PrivateAPI]
        Task<APIResponse> MarkReadAsync(string guid);

        /// <summary>
        /// Adds a participant to an existing chat.
        /// </summary>
        /// <remarks>
        /// Requests with <c>POST /api/v1/chat/:guid/participant/add</c>.
        /// <para/>
        /// Only works with the Private API enabled.
        /// </remarks>
        /// <param name="guid">The chat GUID.</param>
        /// <param name="request">The parameters for this request.</param>
        /// <returns>A response containing the updated chat entity.</returns>
        [PrivateAPI]
        APIResponse<ChatEntity> AddParticipant(string guid, ChatParticipantRequest request);

        /// <inheritdoc cref="AddParticipant"/>
        [PrivateAPI]
        Task<APIResponse<ChatEntity>> AddParticipantAsync(string guid, ChatParticipantRequest request);

        /// <summary>
        /// Removes a participant from an existing chat.
        /// </summary>
        /// <remarks>
        /// Requests with <c>POST /api/v1/chat/:guid/participant/remove</c>.
        /// <para/>
        /// Only works with the Private API enabled.
        /// </remarks>
        /// <param name="guid">The chat GUID.</param>
        /// <param name="request">The parameters for this request.</param>
        /// <returns>A response containing the updated chat entity.</returns>
        [PrivateAPI]
        APIResponse<ChatEntity> RemoveParticipant(string guid, ChatParticipantRequest request);

        /// <inheritdoc cref="RemoveParticipant"/>
        [PrivateAPI]
        Task<APIResponse<ChatEntity>> RemoveParticipantAsync(string guid, ChatParticipantRequest request);

        /// <summary>
        /// Fetches a group chat's icon. This will only return an icon if the group has one set,
        /// and if they have set it at some point after the inception of the macOS host.
        /// </summary>
        /// <remarks>
        /// Requests with <c>GET /api/v1/chat/:guid/icon</c>.
        /// </remarks>
        /// <param name="guid">The chat GUID.</param>
        /// <returns>A stream for receiving the icon data.</returns>
        Stream GetGroupIcon(string guid);

        /// <inheritdoc cref="GetGroupIcon"/>
        Task<Stream> GetGroupIconAsync(string guid);

        /// <summary>
        /// Updates an existing chat.
        /// </summary>
        /// <remarks>
        /// Requests with <c>PUT /api/v1/chat/:guid</c>.
        /// <para/>
        /// Only works with the Private API enabled.
        /// </remarks>
        /// <param name="guid">The chat GUID.</param>
        /// <param name="request">The parameters for this request.</param>
        /// <returns>A response containing the updated chat entity.</returns>
        [PrivateAPI]
        APIResponse<ChatEntity> Update(string guid, ChatUpdateRequest request);

        /// <inheritdoc cref="Update"/>
        [PrivateAPI]
        Task<APIResponse<ChatEntity>> UpdateAsync(string guid, ChatUpdateRequest request);

        /// <summary>
        /// Fetches a chat's database information by GUID.
        /// </summary>
        /// <remarks>
        /// Requests with <c>GET /api/v1/chat/:guid</c>.
        /// </remarks>
        /// <param name="guid">The chat GUID.</param>
        /// <param name="with">The types of values to return.</param>
        /// <returns>A response containing the chat entity.</returns>
        APIResponse<ChatEntity> Find(string guid, params string[] with);

        /// <inheritdoc cref="Find"/>
        Task<APIResponse<ChatEntity>> FindAsync(string guid, params string[] with);

        /// <summary>
        /// Deletes an existing chat.
        /// </summary>
        /// <remarks>
        /// Requests with <c>DELETE /api/v1/chat/:guid</c>.
        /// <para/>
        /// Only works with the Private API enabled.
        /// </remarks>
        /// <param name="guid">The chat GUID.</param>
        /// <returns>A response with no data.</returns>
        [PrivateAPI]
        APIResponse Delete(string guid);

        /// <inheritdoc cref="Delete"/>
        [PrivateAPI]
        Task<APIResponse> DeleteAsync(string guid);
    }

#pragma warning disable SA1600
    internal sealed class ChatImpl : IChat
    {
        public const string CreateUrlPath = "/api/v1/chat/new";
        public const string CountUrlPath = "/api/v1/chat/count";
        public const string QueryUrlPath = "/api/v1/chat/query";
        public const string GetMessagesUrlPath = "/api/v1/chat/{0}/message";
        public const string MarkReadUrlPath = "/api/v1/chat/{0}/read";
        public const string AddParticipantUrlPath = "/api/v1/chat/{0}/participant/add";
        public const string RemoveParticipantUrlPath = "/api/v1/chat/{0}/participant/remove";
        public const string GetGroupIconUrlPath = "/api/v1/chat/{0}/icon";
        public const string ChatUrlPath = "/api/v1/chat/{0}"; // Update, find, delete

        private BlueBubblesClient client;
        public ChatImpl(BlueBubblesClient c) => client = c;

        public APIResponse<ChatEntity> Create(ChatCreateRequest request)
            => client.RequestPost<ChatEntity, ChatCreateRequest>(CreateUrlPath, request);
        public Task<APIResponse<ChatEntity>> CreateAsync(ChatCreateRequest request)
            => client.RequestPostAsync<ChatEntity, ChatCreateRequest>(CreateUrlPath, request);

        public APIResponse<ChatCountResponse> Count()
            => client.RequestGet<ChatCountResponse>(CountUrlPath);
        public Task<APIResponse<ChatCountResponse>> CountAsync()
            => client.RequestGetAsync<ChatCountResponse>(CountUrlPath);

        public APIResponse<ChatEntity[], QueryMetadata> Query(ChatQueryRequest request)
            => client.RequestPost<ChatEntity[], QueryMetadata, ChatQueryRequest>(QueryUrlPath, request);
        public Task<APIResponse<ChatEntity[], QueryMetadata>> QueryAsync(ChatQueryRequest request)
            => client.RequestPostAsync<ChatEntity[], QueryMetadata, ChatQueryRequest>(QueryUrlPath, request);

        public APIResponse<MessageEntity[]> GetMessages(string guid, long? before, long? after, int? limit, int? offset, string sort, params string[] with)
            => client.RequestGet<MessageEntity[]>(Utils.StringFormatUriEncode(GetMessagesUrlPath, guid), Utils.BuildUrlQuery(new { with = string.Join(",", with), before, after, limit, offset, sort }));
        public Task<APIResponse<MessageEntity[]>> GetMessagesAsync(string guid, long? before, long? after, int? limit, int? offset, string sort, params string[] with)
            => client.RequestGetAsync<MessageEntity[]>(Utils.StringFormatUriEncode(GetMessagesUrlPath, guid), Utils.BuildUrlQuery(new { with = string.Join(",", with), before, after, limit, offset, sort }));

        public APIResponse MarkRead(string guid)
            => client.RequestPost(Utils.StringFormatUriEncode(MarkReadUrlPath, guid));
        public Task<APIResponse> MarkReadAsync(string guid)
            => client.RequestPostAsync(Utils.StringFormatUriEncode(MarkReadUrlPath, guid));

        public APIResponse<ChatEntity> AddParticipant(string guid, ChatParticipantRequest request)
            => client.RequestPost<ChatEntity, ChatParticipantRequest>(Utils.StringFormatUriEncode(AddParticipantUrlPath, guid), request);
        public Task<APIResponse<ChatEntity>> AddParticipantAsync(string guid, ChatParticipantRequest request)
            => client.RequestPostAsync<ChatEntity, ChatParticipantRequest>(Utils.StringFormatUriEncode(AddParticipantUrlPath, guid), request);

        public APIResponse<ChatEntity> RemoveParticipant(string guid, ChatParticipantRequest request)
            => client.RequestPost<ChatEntity, ChatParticipantRequest>(Utils.StringFormatUriEncode(RemoveParticipantUrlPath, guid), request);
        public Task<APIResponse<ChatEntity>> RemoveParticipantAsync(string guid, ChatParticipantRequest request)
            => client.RequestPostAsync<ChatEntity, ChatParticipantRequest>(Utils.StringFormatUriEncode(RemoveParticipantUrlPath, guid), request);

        public Stream GetGroupIcon(string guid)
            => client.CreateRequest("GET", Utils.StringFormatUriEncode(GetGroupIconUrlPath, guid), null)
                .GetResponse().GetResponseStream();
        public async Task<Stream> GetGroupIconAsync(string guid)
            => (await client.CreateRequest("GET", Utils.StringFormatUriEncode(GetGroupIconUrlPath, guid), null)
                .GetResponseAsync()).GetResponseStream();

        public APIResponse<ChatEntity> Update(string guid, ChatUpdateRequest request)
            => client.RequestPut<ChatEntity, ChatUpdateRequest>(Utils.StringFormatUriEncode(ChatUrlPath, guid), request);
        public Task<APIResponse<ChatEntity>> UpdateAsync(string guid, ChatUpdateRequest request)
            => client.RequestPutAsync<ChatEntity, ChatUpdateRequest>(Utils.StringFormatUriEncode(ChatUrlPath, guid), request);

        public APIResponse<ChatEntity> Find(string guid, params string[] with)
            => client.RequestGet<ChatEntity>(Utils.StringFormatUriEncode(ChatUrlPath, guid), Utils.BuildUrlQuery(new { with = string.Join(",", with) }));
        public Task<APIResponse<ChatEntity>> FindAsync(string guid, params string[] with)
            => client.RequestGetAsync<ChatEntity>(Utils.StringFormatUriEncode(ChatUrlPath, guid), Utils.BuildUrlQuery(new { with = string.Join(",", with) }));

        public APIResponse Delete(string guid)
            => client.RequestDelete(Utils.StringFormatUriEncode(ChatUrlPath, guid));
        public Task<APIResponse> DeleteAsync(string guid)
            => client.RequestDeleteAsync(Utils.StringFormatUriEncode(ChatUrlPath, guid));
    }
#pragma warning restore SA1600
}
