// This file is a part of the BlueBubbles .NET API library
// Copyright (c) 2022 Dylan Briedis <dylan@dylanbriedis.com>

using BlueBubbles.API.Models.Chat;
using BlueBubbles.API.Models.Message;

namespace BlueBubbles.API.Models.Handle
{
    /// <summary>
    /// Represents a recipient handle.
    /// </summary>
    /// <seealso href="https://github.com/BlueBubblesApp/bluebubbles-server/blob/9572e21180c95587b45dd3cbe4f7568a0bf47661/packages/server/src/server/types.ts#L64"/>
    public class HandleEntity : BaseEntity
    {
        /// <summary>
        /// Gets the messages associated with the handle.
        /// </summary>
        public MessageEntity[] Messages { get; private set; }

        /// <summary>
        /// Gets the chats associated with the handle.
        /// </summary>
        public ChatEntity[] Chats { get; private set; }

        /// <summary>
        /// Gets the handle's address such as its phone number or email address.
        /// </summary>
        public string Address { get; private set; }

        /// <summary>
        /// Gets the handle's country.
        /// </summary>
        public string Country { get; private set; }

        /// <summary>
        /// Gets the handle's uncanonicalized ID.
        /// </summary>
        public string UncanonicalizedId { get; private set; }
    }
}
