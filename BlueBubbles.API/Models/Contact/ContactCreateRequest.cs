// This file is a part of the BlueBubbles .NET API library
// Copyright (c) 2022 Dylan Briedis <dylan@dylanbriedis.com>

using System;
using System.Collections.Generic;

namespace BlueBubbles.API.Models.Contact
{
    /// <summary>
    /// Request model for <c>POST /api/v1/contact</c>.
    /// </summary>
    public sealed class ContactCreateRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContactCreateRequest"/> class.
        /// </summary>
        public ContactCreateRequest() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContactCreateRequest"/> class.
        /// </summary>
        /// <param name="firstName">The contact's first name.</param>
        /// <param name="lastName">The contact's last name.</param>
        public ContactCreateRequest(string firstName, string lastName)
        {
            FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
        }

        /// <summary>
        /// Gets or sets the contact's first name.
        /// </summary>
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the contact's last name.
        /// </summary>
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the contact's display name.
        /// </summary>
        public string DisplayName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the contact's list of phone numbers.
        /// </summary>
        public List<string> PhoneNumbers { get; set; } = new List<string>();

        /// <summary>
        /// Gets or sets the contact's list of emails.
        /// </summary>
        public List<string> Emails { get; set; } = new List<string>();
    }
}
