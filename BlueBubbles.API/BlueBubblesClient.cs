// This file is a part of the BlueBubbles .NET API library
// Copyright (c) 2022 Dylan Briedis <dylan@dylanbriedis.com>

using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using BlueBubbles.API.Interfaces;
using JsonNet.ContractResolvers;
using Newtonsoft.Json;

namespace BlueBubbles.API
{
    /// <summary>
    /// Represents the BlueBubbles API client.
    /// </summary>
    public sealed class BlueBubblesClient
    {
        /// <summary>
        /// Defines the default content type for requests.
        /// </summary>
        public const string DefaultContentType = "application/json";

        private static readonly JsonSerializerSettings SerializerSettings = new JsonSerializerSettings()
        {
            ContractResolver = new PrivateSetterCamelCasePropertyNamesContractResolver(),
        };

        /// <summary>
        /// Initializes a new instance of the <see cref="BlueBubblesClient"/> class.
        /// </summary>
        /// <param name="url">The server URL to connect to.</param>
        /// <param name="password">The password used to authenticate access to the server.</param>
        public BlueBubblesClient(Uri url, string password)
        {
            ServerUrl = url ?? throw new ArgumentNullException(nameof(url));
            ServerPassword = password ?? throw new ArgumentNullException(nameof(password));

            General = new GeneralImpl(this);
            MacOS = new MacOSImpl(this);
            Server = new ServerImpl(this);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BlueBubblesClient"/> class.
        /// </summary>
        /// <param name="url">The server URL to connect to.</param>
        /// <param name="password">The password used to authenticate access to the server.</param>
        public BlueBubblesClient(string url, string password)
            : this(new Uri(url ?? throw new ArgumentNullException(nameof(url))), password) { }

        /// <summary>
        /// Gets or sets the server URL.
        /// </summary>
        public Uri ServerUrl { get; set; }
        /// <summary>
        /// Gets or sets the server password.
        /// </summary>
        public string ServerPassword { get; set; }

        /// <summary>
        /// Gets the General API interface.
        /// </summary>
        public IGeneral General { get; }
        /// <summary>
        /// Gets the MacOS API interface.
        /// </summary>
        public IMacOS MacOS { get; }
        /// <summary>
        /// Gets the Server API interface.
        /// </summary>
        public IServer Server { get; }

        /// <summary>
        /// Creates a web request with the login and the specified parameters added.
        /// </summary>
        /// <param name="method">The protocol method to use.</param>
        /// <param name="path">The path URL for the request.</param>
        /// <param name="query">The query for the URL.</param>
        /// <returns>A <see cref="HttpWebRequest"/> instance for custom response data objects.</returns>
        public HttpWebRequest CreateRequest(string method, string path, string query)
        {
            var uriBuilder = new UriBuilder(ServerUrl)
            {
                Path = path,
                Query = $"password={Uri.EscapeDataString(ServerPassword)}"
                    + (!string.IsNullOrEmpty(query) ? $"&{query}" : string.Empty),
            };

            HttpWebRequest webReq = WebRequest.CreateHttp(uriBuilder.Uri);
            webReq.Method = method;
            return webReq;
        }

        /// <summary>
        /// Initiates a synchronous request to the BlueBubbles server.
        /// </summary>
        /// <typeparam name="TResponse">The type to use for the response model.</typeparam>
        /// <typeparam name="TBody">The type to use for the request model.</typeparam>
        /// <param name="method">The protocol method to use.</param>
        /// <param name="path">The path URL for the request.</param>
        /// <param name="query">The query for the URL.</param>
        /// <param name="body">
        /// An object specified by <typeparamref name="TBody"/> to send.
        /// <see langword="null"/> if <paramref name="method"/> is set to <c>GET</c>.
        /// </param>
        /// <param name="contentType">The body content type.</param>
        /// <returns>
        /// A response object containing the HTTP status code, a status message,
        /// the <typeparamref name="TResponse"/> data, and an error object if failed.
        /// </returns>
        public APIResponse<TResponse> Request<TResponse, TBody>(string method, string path, string query, TBody body, string contentType = DefaultContentType)
        {
            HttpWebRequest webReq = CreateRequest(method, path, query);

            if (body != null)
            {
                webReq.ContentType = "application/json";
                using (var writer = new StreamWriter(webReq.GetRequestStream()))
                {
                    switch (contentType)
                    {
                        case "application/json":
                    writer.Write(JsonConvert.SerializeObject(body, SerializerSettings));
                            break;

                        case "multipart/form-data":
                            Utils.WriteMultipartFormDataTo(writer.BaseStream, body, out string boundary);
                            contentType += "; boundary=" + boundary;
                            break;

                        default:
                            if (body is byte[] bytes)
                            {
                                using (var stream = webReq.GetRequestStream())
                                {
                                    stream.Write(bytes, 0, bytes.Length);
                                }
                            }
                            else
                            {
                                writer.Write(body);
                            }

                            break;
                    }
                }

                webReq.ContentType = contentType;
            }

            Exception exception = null;
            HttpWebResponse webResp;
            try
            {
                webResp = webReq.GetResponse() as HttpWebResponse;
            }
            catch (WebException ex)
            {
                exception = ex;
                webResp = ex.Response as HttpWebResponse;

                if (webResp == null)
                {
                    throw;
                }
            }

            using (webResp)
            using (var reader = new StreamReader(webResp.GetResponseStream()))
            {
                string json = reader.ReadToEnd();
                var resp = JsonConvert.DeserializeObject<APIResponse<TResponse>>(json, SerializerSettings);
                resp.Exception = exception;
                resp.RawJson = json;
                return resp;
            }
        }

        /// <summary>
        /// Initiates an asynchronous request to the BlueBubbles server.
        /// </summary>
        /// <inheritdoc cref="Request"/>
        public Task<APIResponse<TResponse>> RequestAsync<TResponse, TBody>(string method, string path, string query, TBody body, string contentType = DefaultContentType)
        {
            return Task.Run(() => Request<TResponse, TBody>(method, path, query, body, contentType));
        }

        /// <summary>
        /// Initiates a synchronous <c>GET</c> request to the BlueBubbles server.
        /// </summary>
        /// <inheritdoc cref="Request"/>
        public APIResponse<TResponse> RequestGet<TResponse>(string path, string query = null) => Request<TResponse, object>("GET", path, query, null);
        /// <summary>
        /// Initiates an asynchronous <c>GET</c> request to the BlueBubbles server.
        /// </summary>
        /// <inheritdoc cref="Request"/>
        public Task<APIResponse<TResponse>> RequestGetAsync<TResponse>(string path, string query = null) => RequestAsync<TResponse, object>("GET", path, query, null);

        /// <summary>
        /// Initiates a synchronous <c>POST</c> request to the BlueBubbles server.
        /// </summary>
        /// <inheritdoc cref="Request"/>
        public APIResponse<TResponse> RequestPost<TResponse>(string path) => Request<TResponse, object>("POST", path, null, null);
        /// <summary>
        /// Initiates an asynchronous <c>POST</c> request to the BlueBubbles server.
        /// </summary>
        /// <inheritdoc cref="Request"/>
        public Task<APIResponse<TResponse>> RequestPostAsync<TResponse>(string path) => RequestAsync<TResponse, object>("POST", path, null, null);
        /// <summary>
        /// Initiates a synchronous <c>POST</c> request to the BlueBubbles server.
        /// </summary>
        /// <inheritdoc cref="Request"/>
        public APIResponse<TResponse> RequestPost<TResponse, TBody>(string path, TBody body, string contentType = DefaultContentType) => Request<TResponse, TBody>("POST", path, null, body, contentType);
        /// <summary>
        /// Initiates an asynchronous <c>POST</c> request to the BlueBubbles server.
        /// </summary>
        /// <inheritdoc cref="Request"/>
        public Task<APIResponse<TResponse>> RequestPostAsync<TResponse, TBody>(string path, TBody body, string contentType = DefaultContentType) => RequestAsync<TResponse, TBody>("POST", path, null, body, contentType);

        /// <summary>
        /// Initiates a synchronous <c>PUT</c> request to the BlueBubbles server.
        /// </summary>
        /// <inheritdoc cref="Request"/>
        public APIResponse<TResponse> RequestPut<TResponse>(string path) => Request<TResponse, object>("PUT", path, null, null);
        /// <summary>
        /// Initiates an asynchronous <c>PUT</c> request to the BlueBubbles server.
        /// </summary>
        /// <inheritdoc cref="Request"/>
        public Task<APIResponse<TResponse>> RequestPutAsync<TResponse>(string path) => RequestAsync<TResponse, object>("PUT", path, null, null);
        /// <summary>
        /// Initiates a synchronous <c>PUT</c> request to the BlueBubbles server.
        /// </summary>
        /// <inheritdoc cref="Request"/>
        public APIResponse<TResponse> RequestPut<TResponse, TBody>(string path, TBody body, string contentType = DefaultContentType) => Request<TResponse, TBody>("PUT", path, null, body, contentType);
        /// <summary>
        /// Initiates an asynchronous <c>PUT</c> request to the BlueBubbles server.
        /// </summary>
        /// <inheritdoc cref="Request"/>
        public Task<APIResponse<TResponse>> RequestPutAsync<TResponse, TBody>(string path, TBody body, string contentType = DefaultContentType) => RequestAsync<TResponse, TBody>("PUT", path, null, body, contentType);

        /// <summary>
        /// Initiates a synchronous <c>DELETE</c> request to the BlueBubbles server.
        /// </summary>
        /// <inheritdoc cref="Request"/>
        public APIResponse<TResponse> RequestDelete<TResponse>(string path) => Request<TResponse, object>("DELETE", path, null, null);
        /// <summary>
        /// Initiates an asynchronous <c>DELETE</c> request to the BlueBubbles server.
        /// </summary>
        /// <inheritdoc cref="Request"/>
        public Task<APIResponse<TResponse>> RequestDeleteAsync<TResponse>(string path) => RequestAsync<TResponse, object>("DELETE", path, null, null);
    }
}
