// This file is a part of the BlueBubbles .NET API library
// Copyright (c) 2022 Dylan Briedis <dylan@dylanbriedis.com>

using BlueBubbles.API.Models.Handle;
using BlueBubbles.API.Models.Message;

namespace BlueBubbles.API.Models.Chat
{
    /// <summary>
    /// Represents a chat conversation.
    /// </summary>
    /// <seealso href="https://github.com/BlueBubblesApp/bluebubbles-server/blob/9572e21180c95587b45dd3cbe4f7568a0bf47661/packages/server/src/server/types.ts#L73"/>
    public class ChatEntity : BaseEntity
    {
        /// <summary>
        /// Gets the chat's GUID.
        /// </summary>
        public string Guid { get; private set; }

        /// <summary>
        /// Gets a list of participants in the chat.
        /// </summary>
        public HandleEntity[] Participants { get; private set; }

        /// <summary>
        /// Gets the messages associated with the chat.
        /// </summary>
        public MessageEntity[] Messages { get; private set; }

        /// <summary>
        /// Gets the last message sent to this chat.
        /// </summary>
        public MessageEntity LastMessage { get; private set; }

        /// <summary>
        /// Gets the style of the chat.
        /// </summary>
        public int Style { get; private set; }

        /// <summary>
        /// Gets the chat identifier.
        /// </summary>
        public string ChatIdentifier { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the chat is archived.
        /// </summary>
        public bool IsArchived { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the chat is filtered.
        /// </summary>
        public bool IsFiltered { get; private set; }

        /// <summary>
        /// Gets the chat's display (group) name if one is set.
        /// </summary>
        public string DisplayName { get; private set; }

        /// <summary>
        /// Gets the chat's group ID.
        /// </summary>
        public string GroupId { get; private set; }
    }
}
