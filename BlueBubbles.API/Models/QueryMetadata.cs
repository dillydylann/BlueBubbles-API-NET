// This file is a part of the BlueBubbles .NET API library
// Copyright (c) 2022 Dylan Briedis <dylan@dylanbriedis.com>

namespace BlueBubbles.API.Models
{
    /// <summary>
    /// Represents the metadata for query responses.
    /// </summary>
    public class QueryMetadata
    {
        /// <summary>
        /// Gets the number representing the limit for the database query.
        /// </summary>
        public int Limit { get; private set; }

        /// <summary>
        /// Gets the number representing the offset for the database query.
        /// </summary>
        public int Offset { get; private set; }

        /// <summary>
        /// Gets the total amount of entities that were returned.
        /// </summary>
        public int Total { get; private set; }
    }
}
