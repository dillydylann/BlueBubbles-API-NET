// This file is a part of the BlueBubbles .NET API library
// Copyright (c) 2022 Dylan Briedis <dylan@dylanbriedis.com>

using System.Collections.Generic;

namespace BlueBubbles.API.Models
{
    /// <summary>
    /// Represents the base class for database query request models.
    /// </summary>
    public class BaseQueryRequest
    {
        /// <summary>
        /// Gets or sets the number representing the limit for the database query.
        /// </summary>
        public int? Limit { get; set; } = null;

        /// <summary>
        /// Gets or sets the number representing the offset for the database query.
        /// </summary>
        public int? Offset { get; set; } = null;

        /// <summary>
        /// Gets or sets the types of values to return.
        /// </summary>
        public List<string> With { get; set; } = new List<string>();
    }
}
