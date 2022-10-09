// This file is a part of the BlueBubbles .NET API library
// Copyright (c) 2022 Dylan Briedis <dylan@dylanbriedis.com>

using System;
using System.Collections.Generic;
using System.Linq;

namespace BlueBubbles.API.Models.Contact
{
    /// <summary>
    /// Request model for <c>POST /api/v1/contact/query</c>.
    /// </summary>
    public sealed class ContactQueryRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContactQueryRequest"/> class
        /// with an empty list of addresses.
        /// </summary>
        public ContactQueryRequest() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContactQueryRequest"/> class.
        /// </summary>
        /// <param name="addresses">The list of one or more addresses to query.</param>
        public ContactQueryRequest(List<string> addresses)
        {
            Addresses = addresses ?? throw new ArgumentNullException(nameof(addresses));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContactQueryRequest"/> class.
        /// </summary>
        /// <param name="addresses">The list of one or more addresses to query.</param>
        public ContactQueryRequest(IEnumerable<string> addresses)
        {
            Addresses = addresses?.ToList() ?? throw new ArgumentNullException(nameof(addresses));
        }

        /// <summary>
        /// Gets or sets the list of one or more addresses to query.
        /// </summary>
        public List<string> Addresses { get; set; } = new List<string>();
    }
}
