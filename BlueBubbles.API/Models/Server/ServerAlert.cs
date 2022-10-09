// This file is a part of the BlueBubbles .NET API library
// Copyright (c) 2022 Dylan Briedis <dylan@dylanbriedis.com>

using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BlueBubbles.API.Models.Server
{
    /// <summary>
    /// Defines the server alert levels.
    /// </summary>
    public enum ServerAlertType
    {
        /// <summary>
        /// Designates informational messages.
        /// </summary>
        [EnumMember(Value = "info")]
        Info,

        /// <summary>
        /// Designates successful messages.
        /// </summary>
        [EnumMember(Value = "success")]
        Success,

        /// <summary>
        /// Designates serious error events.
        /// </summary>
        [EnumMember(Value = "error")]
        Error,

        /// <summary>
        /// Designates unexpected events.
        /// </summary>
        [EnumMember(Value = "warn")]
        Warning,
    }

    /// <summary>
    /// Represents a server alert.
    /// </summary>
    public class ServerAlert
    {
        /// <summary>
        /// Gets the alert ID.
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Gets the alert type.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public ServerAlertType Type { get; private set; }

        /// <summary>
        /// Gets the alert message.
        /// </summary>
        public string Value { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the alert was read.
        /// </summary>
        public bool IsRead { get; private set; }

        /// <summary>
        /// Gets a date and time of when the alert was created.
        /// </summary>
        public DateTime Created { get; private set; }

        /// <summary>
        /// Gets a date and time of when the alert was updated.
        /// </summary>
        public DateTime Updated { get; private set; }
    }
}
