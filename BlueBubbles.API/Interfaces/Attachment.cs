// This file is a part of the BlueBubbles .NET API library
// Copyright (c) 2022 Dylan Briedis <dylan@dylanbriedis.com>

using System.IO;
using System.Threading.Tasks;
using BlueBubbles.API.Models.Attachment;

namespace BlueBubbles.API.Interfaces
{
    /// <summary>
    /// Contains methods for the Attachment API interface.
    /// </summary>
    public interface IAttachment
    {
        /// <summary>
        /// Fetches the total number of attachments on the server.
        /// </summary>
        /// <remarks>
        /// Requests with <c>GET /api/v1/attachment/count</c>.
        /// </remarks>
        /// <returns>A response containing the total number of attachments.</returns>
        APIResponse<AttachmentCountResponse> Count();

        /// <inheritdoc cref="Count"/>
        Task<APIResponse<AttachmentCountResponse>> CountAsync();

        /// <summary>
        /// Downloads an attachment by GUID.
        /// </summary>
        /// <remarks>
        /// Requests with <c>GET /api/v1/attachment/:guid/download</c>.
        /// </remarks>
        /// <param name="guid">The attachment GUID.</param>
        /// <param name="width">
        /// Specifies the value to resize the width to.
        /// This aspect ratio will be maintained if a height is not present.
        /// </param>
        /// <param name="height">
        /// Specifies the value to resize the height to.
        /// This aspect ratio will be maintained if a width is not present.
        /// </param>
        /// <param name="quality">
        /// Changes the quality of the image.
        /// <para/>
        /// Must be one of: <c>good</c>, <c>better</c>, or <c>best</c>.
        /// </param>
        /// <param name="original">Downloads the original attachment (useful for auto-converted attachments like HEIC and CAF).</param>
        /// <returns>A stream for receiving the attachment data.</returns>
        Stream Download(string guid, int? width = null, int? height = null, string quality = null, bool original = false);

        /// <inheritdoc cref="Download"/>
        Task<Stream> DownloadAsync(string guid, int? width = null, int? height = null, string quality = null, bool original = false);

        /// <summary>
        /// Generates a <see href="https://blurha.sh/">BlurHash</see> string based on the given attachment by GUID.
        /// <para/>
        /// Note: Calculating a BlurHash is fairly intensive.Resizing the image to smaller size by
        /// specifying and/or height will reduce the time it takes to generate the hash string.
        /// </summary>
        /// <remarks>
        /// Requests with <c>GET /api/v1/attachment/:guid/blurhash</c>.
        /// </remarks>
        /// <param name="guid">The attachment GUID.</param>
        /// <param name="width">
        /// Specifies the value to resize the width to.
        /// This aspect ratio will be maintained if a height is not present.
        /// </param>
        /// <param name="height">
        /// Specifies the value to resize the height to.
        /// This aspect ratio will be maintained if a width is not present.
        /// </param>
        /// <param name="quality">
        /// Changes the quality of the image.
        /// <para/>
        /// Must be one of: <c>good</c>, <c>better</c>, or <c>best</c>.
        /// </param>
        /// <returns>A response with the BlurHash of the attachment.</returns>
        APIResponse<string> BlurHash(string guid, int? width = null, int? height = null, string quality = null);

        /// <inheritdoc cref="BlurHash"/>
        Task<APIResponse<string>> BlurHashAsync(string guid, int? width = null, int? height = null, string quality = null);

        /// <summary>
        /// Fetches an attachment's database information by GUID.
        /// </summary>
        /// <remarks>
        /// Requests with <c>GET /api/v1/attachment/:guid</c>.
        /// </remarks>
        /// <param name="guid">The attachment GUID.</param>
        /// <returns>A response containing the attachment entity.</returns>
        APIResponse<AttachmentEntity> Find(string guid);

        /// <inheritdoc cref="Find"/>
        Task<APIResponse<AttachmentEntity>> FindAsync(string guid);
    }

#pragma warning disable SA1600
    internal sealed class AttachmentImpl : IAttachment
    {
        public const string CountUrlPath = "/api/v1/attachment/count";
        public const string DownloadUrlPath = "/api/v1/attachment/{0}/download";
        public const string BlurHashUrlPath = "/api/v1/attachment/{0}/blurhash";
        public const string FindUrlPath = "/api/v1/attachment/{0}";

        private BlueBubblesClient client;
        public AttachmentImpl(BlueBubblesClient c) => client = c;

        public APIResponse<AttachmentCountResponse> Count()
            => client.RequestGet<AttachmentCountResponse>(CountUrlPath);
        public Task<APIResponse<AttachmentCountResponse>> CountAsync()
            => client.RequestGetAsync<AttachmentCountResponse>(CountUrlPath);

        public Stream Download(string guid, int? width, int? height, string quality, bool original)
            => client.CreateRequest("GET", Utils.StringFormatUriEncode(DownloadUrlPath, guid), Utils.BuildUrlQuery(new { width, height, quality, original }))
                .GetResponse().GetResponseStream();
        public async Task<Stream> DownloadAsync(string guid, int? width, int? height, string quality, bool original)
            => (await client.CreateRequest("GET", Utils.StringFormatUriEncode(DownloadUrlPath, guid), Utils.BuildUrlQuery(new { width, height, quality, original }))
                .GetResponseAsync()).GetResponseStream();

        public APIResponse<string> BlurHash(string guid, int? width, int? height, string quality)
            => client.RequestGet<string>(Utils.StringFormatUriEncode(BlurHashUrlPath, guid), Utils.BuildUrlQuery(new { width, height, quality }));
        public Task<APIResponse<string>> BlurHashAsync(string guid, int? width, int? height, string quality)
            => client.RequestGetAsync<string>(Utils.StringFormatUriEncode(BlurHashUrlPath, guid), Utils.BuildUrlQuery(new { width, height, quality }));

        public APIResponse<AttachmentEntity> Find(string guid)
            => client.RequestGet<AttachmentEntity>(Utils.StringFormatUriEncode(FindUrlPath, guid));
        public Task<APIResponse<AttachmentEntity>> FindAsync(string guid)
            => client.RequestGetAsync<AttachmentEntity>(Utils.StringFormatUriEncode(FindUrlPath, guid));
    }
#pragma warning restore SA1600
}
