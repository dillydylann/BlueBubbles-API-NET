// This file is a part of the BlueBubbles .NET API library
// Copyright (c) 2022 Dylan Briedis <dylan@dylanbriedis.com>

using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace BlueBubbles.API.Models.Message
{
    /// <summary>
    /// Defines how data should be sorted.
    /// </summary>
    public enum MessageQuerySort
    {
        /// <summary>
        /// Sorts the data in ascending order.
        /// </summary>
        [EnumMember(Value = "ASC")]
        Ascending,

        /// <summary>
        /// Sorts the data in descending order.
        /// </summary>
        [EnumMember(Value = "DESC")]
        Descending,
    }

    /// <summary>
    /// Request model for <c>POST /api/v1/message/query</c>.
    /// </summary>
    public sealed class MessageQueryRequest : BaseQueryRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MessageQueryRequest"/> class.
        /// </summary>
        public MessageQueryRequest() { }

        /// <summary>
        /// Gets or sets the database <c>WHERE</c> clauses allowing to granularly filter down or search for messages.
        /// <para/>
        /// This should follow TypeORM's WHERE expression query builder.
        /// <br/>
        /// Reference: <see href="https://typeorm.io/select-query-builder#adding-where-expression"/>.
        /// </summary>
        public JArray Where { get; set; } = null;

        /// <summary>
        /// Gets or sets the date to fetch messages before, represented in seconds since EPOCH.
        /// </summary>
        public long? After { get; set; } = null;

        /// <summary>
        /// Gets or sets the date to fetch messages after, represented in seconds since EPOCH.
        /// </summary>
        public long? Before { get; set; } = null;

        /// <summary>
        /// Gets or sets how the data should be sorted.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public MessageQuerySort Sort { get; set; } = MessageQuerySort.Descending;
    }
}
