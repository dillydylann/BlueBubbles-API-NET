// This file is a part of the BlueBubbles .NET API library
// Copyright (c) 2022 Dylan Briedis <dylan@dylanbriedis.com>

namespace BlueBubbles.API.Models.Contact
{
    /// <summary>
    /// Represents a contact.
    /// </summary>
    public class ContactObject
    {
        /// <summary>
        /// Gets the contact's list of phone numbers.
        /// </summary>
        public AddressData[] PhoneNumbers { get; private set; }

        /// <summary>
        /// Gets the contact's list of email addresses.
        /// </summary>
        public AddressData[] Emails { get; private set; }

        /// <summary>
        /// Gets the contact's first name.
        /// </summary>
        public string FirstName { get; private set; }

        /// <summary>
        /// Gets the contact's last name.
        /// </summary>
        public string LastName { get; private set; }

        /// <summary>
        /// Gets the contact's display name.
        /// </summary>
        public string DisplayName { get; private set; }

        /// <summary>
        /// Gets the contact's birthday.
        /// </summary>
        public string Birthday { get; private set; }

        /// <summary>
        /// Gets the contact's source type.
        /// </summary>
        public string SourceType { get; private set; }

        /// <summary>
        /// Gets the contact's ID.
        /// </summary>
        public string Id { get; private set; }

        /// <summary>
        /// Represents a contact's phone number or email address.
        /// </summary>
        public sealed class AddressData
        {
            /// <summary>
            /// Gets the address.
            /// </summary>
            public string Address { get; private set; }

            /// <summary>
            /// Gets the ID of the address.
            /// </summary>
            public string Id { get; private set; }
        }
    }
}
