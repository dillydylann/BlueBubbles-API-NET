// This file is a part of the BlueBubbles .NET API library
// Copyright (c) 2022 Dylan Briedis <dylan@dylanbriedis.com>

using System;
using System.Collections.Generic;
using System.Linq;

namespace BlueBubbles.API.Models.Handle
{
    /// <summary>
    /// Request model for <c>POST /api/v1/handle/query</c>.
    /// </summary>
    public sealed class HandleQueryRequest : BaseQueryRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HandleQueryRequest"/> class.
        /// </summary>
        public HandleQueryRequest() { Offset = 0; }

        /// <summary>
        /// Gets or sets the address to query.
        /// </summary>
        public string Address { get; set; } = string.Empty;
    }
}
