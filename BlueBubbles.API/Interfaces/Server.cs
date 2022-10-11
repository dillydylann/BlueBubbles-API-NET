// This file is a part of the BlueBubbles .NET API library
// Copyright (c) 2022 Dylan Briedis <dylan@dylanbriedis.com>

using System.Threading.Tasks;
using BlueBubbles.API.Models.Server;

namespace BlueBubbles.API.Interfaces
{
    /// <summary>
    /// Contains methods for the Server API interface.
    /// </summary>
    public interface IServer
    {
        /// <summary>
        /// Fetches information about the server and its operating system.
        /// </summary>
        /// <remarks>
        /// Requests with <c>GET /api/v1/server/info</c>.
        /// </remarks>
        /// <returns>A response with the server info.</returns>
        APIResponse<ServerInfoResponse> GetInfo();

        /// <inheritdoc cref="GetInfo"/>
        Task<APIResponse<ServerInfoResponse>> GetInfoAsync();

        /// <summary>
        /// Fetches logs from the server.
        /// </summary>
        /// <remarks>
        /// Requests with <c>GET /api/v1/server/logs</c>.
        /// </remarks>
        /// <returns>A response with a string of server logs.</returns>
        APIResponse<string> GetLogs();

        /// <inheritdoc cref="GetLogs"/>
        Task<APIResponse<string>> GetLogsAsync();

        /// <summary>
        /// Restarts the server services while keeping the server process running (soft restart).
        /// </summary>
        /// <remarks>
        /// Requests with <c>GET /api/v1/server/restart/soft</c>.
        /// </remarks>
        /// <returns>A response with no data.</returns>
        APIResponse RestartServices();

        /// <inheritdoc cref="RestartServices"/>
        Task<APIResponse> RestartServicesAsync();

        /// <summary>
        /// Restarts the entire server process (hard restart).
        /// </summary>
        /// <remarks>
        /// Requests with <c>GET /api/v1/server/restart/hard</c>.
        /// </remarks>
        /// <returns>A response with no data.</returns>
        APIResponse RestartAll();

        /// <inheritdoc cref="RestartAll"/>
        Task<APIResponse> RestartAllAsync();

        /// <summary>
        /// Fetches alerts from the server.
        /// </summary>
        /// <remarks>
        /// Requests with <c>GET /api/v1/server/alert</c>.
        /// </remarks>
        /// <returns>A response with an array of alerts.</returns>
        APIResponse<ServerAlert[]> GetAlerts();

        /// <inheritdoc cref="GetAlerts"/>
        Task<APIResponse<ServerAlert[]>> GetAlertsAsync();

        /// <summary>
        /// Marks specified alerts on the server. This requires the IDs of the alerts
        /// because of race cases making a "catch-all" endpoint to mark all alerts as read.
        /// </summary>
        /// <remarks>
        /// Requests with <c>POST /api/v1/server/alert/read</c>.
        /// </remarks>
        /// <param name="request">The parameters for this request.</param>
        /// <returns>A response with no data.</returns>
        APIResponse MarkAlertsAsRead(ServerMarkAlertsAsReadRequest request);

        /// <inheritdoc cref="GetAlerts"/>
        Task<APIResponse> MarkAlertsAsReadAsync(ServerMarkAlertsAsReadRequest request);

        /// <summary>
        /// Fetches the database totals for the iMessage entities.
        /// </summary>
        /// <remarks>
        /// Requests with <c>GET /api/v1/server/statistics/totals</c>.
        /// </remarks>
        /// <returns>A response containing the total amount of iMessage entities on the server.</returns>
        APIResponse<ServerStatTotalsResponse> GetStatTotals();

        /// <inheritdoc cref="GetAlerts"/>
        Task<APIResponse<ServerStatTotalsResponse>> GetStatTotalsAsync();

        /// <summary>
        /// Fetches different types of media totals for all chats (i.e. videos and images).
        /// </summary>
        /// <remarks>
        /// Requests with <c>GET /api/v1/server/statistics/media</c>.
        /// </remarks>
        /// <returns>A response containing the total amount of media entities on the server.</returns>
        APIResponse<ServerStatMediaResponse> GetStatMedia();

        /// <inheritdoc cref="GetAlerts"/>
        Task<APIResponse<ServerStatMediaResponse>> GetStatMediaAsync();

        /// <summary>
        /// Fetches different types of media totals per chat (i.e. videos and images).
        /// </summary>
        /// <remarks>
        /// Requests with <c>GET /api/v1/server/statistics/media/chat</c>.
        /// </remarks>
        /// <returns>A response containing an array of total amounts of media entities per chat.</returns>
        APIResponse<ServerStatMediaByChatTotals[]> GetStatMediaByChat();

        /// <inheritdoc cref="GetAlerts"/>
        Task<APIResponse<ServerStatMediaByChatTotals[]>> GetStatMediaByChatAsync();
    }

#pragma warning disable SA1600
    internal sealed class ServerImpl : IServer
    {
        public const string GetInfoUrlPath = "/api/v1/server/info";
        public const string GetLogsUrlPath = "/api/v1/server/logs";
        public const string RestartServicesUrlPath = "/api/v1/server/restart/soft";
        public const string RestartAllUrlPath = "/api/v1/server/restart/hard";
        public const string CheckForUpdateUrlPath = "/api/v1/server/update/check";
        public const string GetAlertsUrlPath = "/api/v1/server/alert";
        public const string MarkAlertsAsReadUrlPath = "/api/v1/server/alert/read";
        public const string GetStatTotalsUrlPath = "/api/v1/server/statistics/totals";
        public const string GetStatMediaUrlPath = "/api/v1/server/statistics/media";
        public const string GetStatMediaByChatUrlPath = "/api/v1/server/statistics/media/chat";

        private BlueBubblesClient client;
        public ServerImpl(BlueBubblesClient c) => client = c;

        public APIResponse<ServerInfoResponse> GetInfo()
            => client.RequestGet<ServerInfoResponse>(GetInfoUrlPath);
        public Task<APIResponse<ServerInfoResponse>> GetInfoAsync()
            => client.RequestGetAsync<ServerInfoResponse>(GetInfoUrlPath);

        public APIResponse<string> GetLogs()
            => client.RequestGet<string>(GetLogsUrlPath);
        public Task<APIResponse<string>> GetLogsAsync()
            => client.RequestGetAsync<string>(GetLogsUrlPath);

        public APIResponse RestartServices()
            => client.RequestGet(RestartServicesUrlPath);
        public Task<APIResponse> RestartServicesAsync()
            => client.RequestGetAsync(RestartServicesUrlPath);

        public APIResponse RestartAll()
            => client.RequestGet(RestartAllUrlPath);
        public Task<APIResponse> RestartAllAsync()
            => client.RequestGetAsync(RestartAllUrlPath);

        public APIResponse<ServerAlert[]> GetAlerts()
            => client.RequestGet<ServerAlert[]>(GetAlertsUrlPath);
        public Task<APIResponse<ServerAlert[]>> GetAlertsAsync()
            => client.RequestGetAsync<ServerAlert[]>(GetAlertsUrlPath);

        public APIResponse MarkAlertsAsRead(ServerMarkAlertsAsReadRequest request)
            => client.RequestPost<ServerMarkAlertsAsReadRequest>(MarkAlertsAsReadUrlPath, request);
        public Task<APIResponse> MarkAlertsAsReadAsync(ServerMarkAlertsAsReadRequest request)
            => client.RequestPostAsync<ServerMarkAlertsAsReadRequest>(MarkAlertsAsReadUrlPath, request);

        public APIResponse<ServerStatTotalsResponse> GetStatTotals()
            => client.RequestGet<ServerStatTotalsResponse>(GetStatTotalsUrlPath);
        public Task<APIResponse<ServerStatTotalsResponse>> GetStatTotalsAsync()
            => client.RequestGetAsync<ServerStatTotalsResponse>(GetStatTotalsUrlPath);

        public APIResponse<ServerStatMediaResponse> GetStatMedia()
            => client.RequestGet<ServerStatMediaResponse>(GetStatMediaUrlPath);
        public Task<APIResponse<ServerStatMediaResponse>> GetStatMediaAsync()
            => client.RequestGetAsync<ServerStatMediaResponse>(GetStatMediaUrlPath);

        public APIResponse<ServerStatMediaByChatTotals[]> GetStatMediaByChat()
            => client.RequestGet<ServerStatMediaByChatTotals[]>(GetStatMediaByChatUrlPath);
        public Task<APIResponse<ServerStatMediaByChatTotals[]>> GetStatMediaByChatAsync()
            => client.RequestGetAsync<ServerStatMediaByChatTotals[]>(GetStatMediaByChatUrlPath);
    }
#pragma warning restore SA1600
}
