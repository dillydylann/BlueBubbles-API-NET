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

            Contact = new ContactImpl(this);
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
        /// Gets the Contact API interface.
        /// </summary>
        public IContact Contact { get; }
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
        /// <typeparam name="TData">The type to use for the response model data.</typeparam>
        /// <typeparam name="TMetadata">The type to use for the response model metadata.</typeparam>
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
        /// the <typeparamref name="TData"/> data, and an error object if failed.
        /// </returns>
        public APIResponse<TData, TMetadata> Request<TData, TMetadata, TBody>(string method, string path, string query, TBody body, string contentType = DefaultContentType)
        {
            HttpWebRequest webReq = CreateRequest(method, path, query);

            if (body != null)
            {
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
                var resp = JsonConvert.DeserializeObject<APIResponse<TData, TMetadata>>(json, SerializerSettings);
                resp.Exception = exception;
                resp.RawJson = json;
                return resp;
            }
        }

        /// <inheritdoc cref="Request"/>
        public APIResponse<TData> Request<TData, TBody>(string method, string path, string query, TBody body, string contentType = DefaultContentType)
        {
            return Request<TData, object, TBody>(method, path, query, body, contentType);
        }

        /// <inheritdoc cref="Request"/>
        public APIResponse Request<TBody>(string method, string path, string query, TBody body, string contentType = DefaultContentType)
        {
            return Request<object, object, TBody>(method, path, query, body, contentType);
        }

        /// <summary>
        /// Initiates an asynchronous request to the BlueBubbles server.
        /// </summary>
        /// <inheritdoc cref="Request"/>
        public Task<APIResponse<TData, TMetadata>> RequestAsync<TData, TMetadata, TBody>(string method, string path, string query, TBody body, string contentType = DefaultContentType)
        {
            return Task.Run(() => Request<TData, TMetadata, TBody>(method, path, query, body, contentType));
        }

        /// <inheritdoc cref="RequestAsync"/>
        public async Task<APIResponse<TData>> RequestAsync<TData, TBody>(string method, string path, string query, TBody body, string contentType = DefaultContentType)
        {
            var task = RequestAsync<TData, object, TBody>(method, path, query, body, contentType);
            await task.ConfigureAwait(false);
            return task.Result;
        }

        /// <inheritdoc cref="RequestAsync"/>
        public async Task<APIResponse> RequestAsync<TBody>(string method, string path, string query, TBody body, string contentType = DefaultContentType)
        {
            var task = RequestAsync<object, object, TBody>(method, path, query, body, contentType);
            await task.ConfigureAwait(false);
            return task.Result;
        }

        /// <summary>
        /// Initiates a synchronous <c>GET</c> request to the BlueBubbles server.
        /// </summary>
        /// <inheritdoc cref="Request"/>
        public APIResponse RequestGet(string path, string query = null) => Request<object>("GET", path, query, null);
        /// <summary>
        /// Initiates an asynchronous <c>GET</c> request to the BlueBubbles server.
        /// </summary>
        /// <inheritdoc cref="Request"/>
        public Task<APIResponse> RequestGetAsync(string path, string query = null) => RequestAsync<object>("GET", path, query, null);
        /// <summary>
        /// Initiates a synchronous <c>GET</c> request to the BlueBubbles server.
        /// </summary>
        /// <inheritdoc cref="Request"/>
        public APIResponse<TData> RequestGet<TData>(string path, string query = null) => Request<TData, object>("GET", path, query, null);
        /// <summary>
        /// Initiates an asynchronous <c>GET</c> request to the BlueBubbles server.
        /// </summary>
        /// <inheritdoc cref="Request"/>
        public Task<APIResponse<TData>> RequestGetAsync<TData>(string path, string query = null) => RequestAsync<TData, object>("GET", path, query, null);
        /// <summary>
        /// Initiates a synchronous <c>GET</c> request to the BlueBubbles server.
        /// </summary>
        /// <inheritdoc cref="Request"/>
        public APIResponse<TData, TMetadata> RequestGet<TData, TMetadata>(string path, string query = null) => Request<TData, TMetadata, object>("GET", path, query, null);
        /// <summary>
        /// Initiates an asynchronous <c>GET</c> request to the BlueBubbles server.
        /// </summary>
        /// <inheritdoc cref="Request"/>
        public Task<APIResponse<TData, TMetadata>> RequestGetAsync<TData, TMetadata>(string path, string query = null) => RequestAsync<TData, TMetadata, object>("GET", path, query, null);

        /// <summary>
        /// Initiates a synchronous <c>POST</c> request to the BlueBubbles server.
        /// </summary>
        /// <inheritdoc cref="Request"/>
        public APIResponse RequestPost(string path) => Request<object>("POST", path, null, null);
        /// <summary>
        /// Initiates an asynchronous <c>POST</c> request to the BlueBubbles server.
        /// </summary>
        /// <inheritdoc cref="Request"/>
        public Task<APIResponse> RequestPostAsync(string path) => RequestAsync<object>("POST", path, null, null);
        /// <summary>
        /// Initiates a synchronous <c>POST</c> request to the BlueBubbles server.
        /// </summary>
        /// <inheritdoc cref="Request"/>
        public APIResponse RequestPost<TBody>(string path, TBody body, string contentType = DefaultContentType) => Request<TBody>("POST", path, null, body, contentType);
        /// <summary>
        /// Initiates an asynchronous <c>POST</c> request to the BlueBubbles server.
        /// </summary>
        /// <inheritdoc cref="Request"/>
        public Task<APIResponse> RequestPostAsync<TBody>(string path, TBody body, string contentType = DefaultContentType) => RequestAsync<TBody>("POST", path, null, body, contentType);
        /// <summary>
        /// Initiates a synchronous <c>POST</c> request to the BlueBubbles server.
        /// </summary>
        /// <inheritdoc cref="Request"/>
        public APIResponse<TData> RequestPost<TData>(string path) => Request<TData, object>("POST", path, null, null);
        /// <summary>
        /// Initiates an asynchronous <c>POST</c> request to the BlueBubbles server.
        /// </summary>
        /// <inheritdoc cref="Request"/>
        public Task<APIResponse<TData>> RequestPostAsync<TData>(string path) => RequestAsync<TData, object>("POST", path, null, null);
        /// <summary>
        /// Initiates a synchronous <c>POST</c> request to the BlueBubbles server.
        /// </summary>
        /// <inheritdoc cref="Request"/>
        public APIResponse<TData> RequestPost<TData, TBody>(string path, TBody body, string contentType = DefaultContentType) => Request<TData, TBody>("POST", path, null, body, contentType);
        /// <summary>
        /// Initiates an asynchronous <c>POST</c> request to the BlueBubbles server.
        /// </summary>
        /// <inheritdoc cref="Request"/>
        public Task<APIResponse<TData>> RequestPostAsync<TData, TBody>(string path, TBody body, string contentType = DefaultContentType) => RequestAsync<TData, TBody>("POST", path, null, body, contentType);
        /// <summary>
        /// Initiates a synchronous <c>POST</c> request to the BlueBubbles server.
        /// </summary>
        /// <inheritdoc cref="Request"/>
        public APIResponse<TData, TMetadata> RequestPost<TData, TMetadata>(string path) => Request<TData, TMetadata, object>("POST", path, null, null);
        /// <summary>
        /// Initiates an asynchronous <c>POST</c> request to the BlueBubbles server.
        /// </summary>
        /// <inheritdoc cref="Request"/>
        public Task<APIResponse<TData, TMetadata>> RequestPostAsync<TData, TMetadata>(string path) => RequestAsync<TData, TMetadata, object>("POST", path, null, null);
        /// <summary>
        /// Initiates a synchronous <c>POST</c> request to the BlueBubbles server.
        /// </summary>
        /// <inheritdoc cref="Request"/>
        public APIResponse<TData, TMetadata> RequestPost<TData, TMetadata, TBody>(string path, TBody body, string contentType = DefaultContentType) => Request<TData, TMetadata, TBody>("POST", path, null, body, contentType);
        /// <summary>
        /// Initiates an asynchronous <c>POST</c> request to the BlueBubbles server.
        /// </summary>
        /// <inheritdoc cref="Request"/>
        public Task<APIResponse<TData, TMetadata>> RequestPostAsync<TData, TMetadata, TBody>(string path, TBody body, string contentType = DefaultContentType) => RequestAsync<TData, TMetadata, TBody>("POST", path, null, body, contentType);

        /// <summary>
        /// Initiates a synchronous <c>PUT</c> request to the BlueBubbles server.
        /// </summary>
        /// <inheritdoc cref="Request"/>
        public APIResponse RequestPut(string path) => Request<object>("PUT", path, null, null);
        /// <summary>
        /// Initiates an asynchronous <c>PUT</c> request to the BlueBubbles server.
        /// </summary>
        /// <inheritdoc cref="Request"/>
        public Task<APIResponse> RequestPutAsync(string path) => RequestAsync<object>("PUT", path, null, null);
        /// <summary>
        /// Initiates a synchronous <c>PUT</c> request to the BlueBubbles server.
        /// </summary>
        /// <inheritdoc cref="Request"/>
        public APIResponse RequestPut<TBody>(string path, TBody body, string contentType = DefaultContentType) => Request<TBody>("PUT", path, null, body, contentType);
        /// <summary>
        /// Initiates an asynchronous <c>PUT</c> request to the BlueBubbles server.
        /// </summary>
        /// <inheritdoc cref="Request"/>
        public Task<APIResponse> RequestPutAsync<TBody>(string path, TBody body, string contentType = DefaultContentType) => RequestAsync<TBody>("PUT", path, null, body, contentType);
        /// <summary>
        /// Initiates a synchronous <c>PUT</c> request to the BlueBubbles server.
        /// </summary>
        /// <inheritdoc cref="Request"/>
        public APIResponse<TData> RequestPut<TData>(string path) => Request<TData, object>("PUT", path, null, null);
        /// <summary>
        /// Initiates an asynchronous <c>PUT</c> request to the BlueBubbles server.
        /// </summary>
        /// <inheritdoc cref="Request"/>
        public Task<APIResponse<TData>> RequestPutAsync<TData>(string path) => RequestAsync<TData, object>("PUT", path, null, null);
        /// <summary>
        /// Initiates a synchronous <c>PUT</c> request to the BlueBubbles server.
        /// </summary>
        /// <inheritdoc cref="Request"/>
        public APIResponse<TData> RequestPut<TData, TBody>(string path, TBody body, string contentType = DefaultContentType) => Request<TData, TBody>("PUT", path, null, body, contentType);
        /// <summary>
        /// Initiates an asynchronous <c>PUT</c> request to the BlueBubbles server.
        /// </summary>
        /// <inheritdoc cref="Request"/>
        public Task<APIResponse<TData>> RequestPutAsync<TData, TBody>(string path, TBody body, string contentType = DefaultContentType) => RequestAsync<TData, TBody>("PUT", path, null, body, contentType);
        /// <summary>
        /// Initiates a synchronous <c>PUT</c> request to the BlueBubbles server.
        /// </summary>
        /// <inheritdoc cref="Request"/>
        public APIResponse<TData, TMetadata> RequestPut<TData, TMetadata>(string path) => Request<TData, TMetadata, object>("PUT", path, null, null);
        /// <summary>
        /// Initiates an asynchronous <c>PUT</c> request to the BlueBubbles server.
        /// </summary>
        /// <inheritdoc cref="Request"/>
        public Task<APIResponse<TData, TMetadata>> RequestPutAsync<TData, TMetadata>(string path) => RequestAsync<TData, TMetadata, object>("PUT", path, null, null);
        /// <summary>
        /// Initiates a synchronous <c>PUT</c> request to the BlueBubbles server.
        /// </summary>
        /// <inheritdoc cref="Request"/>
        public APIResponse<TData, TMetadata> RequestPut<TData, TMetadata, TBody>(string path, TBody body, string contentType = DefaultContentType) => Request<TData, TMetadata, TBody>("PUT", path, null, body, contentType);
        /// <summary>
        /// Initiates an asynchronous <c>PUT</c> request to the BlueBubbles server.
        /// </summary>
        /// <inheritdoc cref="Request"/>
        public Task<APIResponse<TData, TMetadata>> RequestPutAsync<TData, TMetadata, TBody>(string path, TBody body, string contentType = DefaultContentType) => RequestAsync<TData, TMetadata, TBody>("PUT", path, null, body, contentType);

        /// <summary>
        /// Initiates a synchronous <c>DELETE</c> request to the BlueBubbles server.
        /// </summary>
        /// <inheritdoc cref="Request"/>
        public APIResponse RequestDelete(string path) => Request<object>("DELETE", path, null, null);
        /// <summary>
        /// Initiates an asynchronous <c>DELETE</c> request to the BlueBubbles server.
        /// </summary>
        /// <inheritdoc cref="Request"/>
        public Task<APIResponse> RequestDeleteAsync(string path) => RequestAsync<object>("DELETE", path, null, null);
        /// <summary>
        /// Initiates a synchronous <c>DELETE</c> request to the BlueBubbles server.
        /// </summary>
        /// <inheritdoc cref="Request"/>
        public APIResponse<TData> RequestDelete<TData>(string path) => Request<TData, object>("DELETE", path, null, null);
        /// <summary>
        /// Initiates an asynchronous <c>DELETE</c> request to the BlueBubbles server.
        /// </summary>
        /// <inheritdoc cref="Request"/>
        public Task<APIResponse<TData>> RequestDeleteAsync<TData>(string path) => RequestAsync<TData, object>("DELETE", path, null, null);
        /// <summary>
        /// Initiates a synchronous <c>DELETE</c> request to the BlueBubbles server.
        /// </summary>
        /// <inheritdoc cref="Request"/>
        public APIResponse<TData, TMetadata> RequestDelete<TData, TMetadata>(string path) => Request<TData, TMetadata, object>("DELETE", path, null, null);
        /// <summary>
        /// Initiates an asynchronous <c>DELETE</c> request to the BlueBubbles server.
        /// </summary>
        /// <inheritdoc cref="Request"/>
        public Task<APIResponse<TData, TMetadata>> RequestDeleteAsync<TData, TMetadata>(string path) => RequestAsync<TData, TMetadata, object>("DELETE", path, null, null);
    }
}
