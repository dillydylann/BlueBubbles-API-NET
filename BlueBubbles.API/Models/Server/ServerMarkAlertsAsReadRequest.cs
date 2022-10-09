// This file is a part of the BlueBubbles .NET API library
// Copyright (c) 2022 Dylan Briedis <dylan@dylanbriedis.com>

using System;
using System.Collections.Generic;
using System.Linq;

namespace BlueBubbles.API.Models.Server
{
    /// <summary>
    /// Request model for <c>POST /api/v1/server/alert/read</c>.
    /// </summary>
    public sealed class ServerMarkAlertsAsReadRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServerMarkAlertsAsReadRequest"/> class
        /// with an empty list of addresses.
        /// </summary>
        public ServerMarkAlertsAsReadRequest() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServerMarkAlertsAsReadRequest"/> class.
        /// </summary>
        /// <param name="ids">The list of one or more IDs to mark as read.</param>
        public ServerMarkAlertsAsReadRequest(List<int> ids)
        {
            Ids = ids ?? throw new ArgumentNullException(nameof(ids));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServerMarkAlertsAsReadRequest"/> class.
        /// </summary>
        /// <param name="ids">The list of one or more IDs to mark as read.</param>
        public ServerMarkAlertsAsReadRequest(IEnumerable<int> ids)
        {
            Ids = ids.ToList() ?? throw new ArgumentNullException(nameof(ids));
        }

        /// <summary>
        /// Gets or sets the list of one or more IDs to mark as read.
        /// </summary>
        public List<int> Ids { get; set; } = new List<int>();
    }
}
