// This file is a part of the BlueBubbles .NET API library
// Copyright (c) 2022 Dylan Briedis <dylan@dylanbriedis.com>

using System;
using System.Net;
using Newtonsoft.Json;

namespace BlueBubbles.API
{
    /// <summary>
    /// Represents an error object from a request.
    /// </summary>
    public sealed class APIError
    {
        /// <summary>
        /// Gets the type of error.
        /// </summary>
        public string Type { get; internal set; }

        /// <summary>
        /// Gets the error message.
        /// </summary>
        public string Message { get; internal set; }
    }

    /// <summary>
    /// Represents a response object from a request.
    /// </summary>
    /// <typeparam name="TData">The type to use for the response model.</typeparam>
    public sealed class APIResponse<TData>
    {
        /// <summary>
        /// Gets the response status code.
        /// </summary>
        public HttpStatusCode Status { get; internal set; }

        /// <summary>
        /// Gets the response message.
        /// </summary>
        public string Message { get; internal set; }

        /// <summary>
        /// Gets the response data.
        /// </summary>
        public TData Data { get; internal set; }

        /// <summary>
        /// Gets the response error.
        /// </summary>
        public APIError Error { get; internal set; }

        /// <summary>
        /// Gets the response exception.
        /// </summary>
        [JsonIgnore]
        public Exception Exception { get; internal set; }

        /// <summary>
        /// Gets the response's raw JSON string.
        /// </summary>
        [JsonIgnore]
        public string RawJson { get; internal set; }
    }
}
