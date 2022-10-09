// This file is a part of the BlueBubbles .NET API library
// Copyright (c) 2022 Dylan Briedis <dylan@dylanbriedis.com>

using System;
using Newtonsoft.Json;

namespace BlueBubbles.API.Models
{
    /// <summary>
    /// Represents the base class for all iMessage entities.
    /// </summary>
    public class BaseEntity : IEquatable<BaseEntity>
    {
        /// <summary>
        /// Gets the original row ID in the server database.
        /// </summary>
        [JsonProperty("originalROWID")]
        public int OriginalRowId { get; private set; }

        /// <inheritdoc/>
        public override bool Equals(object obj) => Equals(obj as BaseEntity);

        /// <inheritdoc/>
        public bool Equals(BaseEntity other) => !(other is null) && OriginalRowId == other.OriginalRowId;

        /// <summary>
        /// Returns the hash code for this object.
        /// </summary>
        /// <inheritdoc path="//returns"/>
        public override int GetHashCode() => -1962740069 + OriginalRowId.GetHashCode();
    }
}
