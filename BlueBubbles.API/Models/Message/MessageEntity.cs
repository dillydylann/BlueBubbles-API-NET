// This file is a part of the BlueBubbles .NET API library
// Copyright (c) 2022 Dylan Briedis <dylan@dylanbriedis.com>

using System.Collections.Generic;
using BlueBubbles.API.Models.Attachment;
using BlueBubbles.API.Models.Chat;
using BlueBubbles.API.Models.Handle;

namespace BlueBubbles.API.Models.Message
{
    /// <summary>
    /// Represents a message.
    /// </summary>
    /// <seealso href="https://github.com/BlueBubblesApp/bluebubbles-server/blob/9572e21180c95587b45dd3cbe4f7568a0bf47661/packages/server/src/server/types.ts#L20"/>
    public class MessageEntity : BaseEntity
    {
        /// <summary>
        /// Gets the message's temporary GUID.
        /// </summary>
        public string TempGuid { get; private set; }

        /// <summary>
        /// Gets the message's GUID.
        /// </summary>
        public string Guid { get; private set; }

        /// <summary>
        /// Gets the message text.
        /// </summary>
        public string Text { get; private set; }

        /// <summary>
        /// Gets the message's attributed body.
        /// </summary>
        public AttributedData[] AttributedBody { get; private set; }

        /// <summary>
        /// Gets the handle of who sent the message.
        /// </summary>
        public HandleEntity Handle { get; private set; }

        /// <summary>
        /// Gets the handle's ID of who sent the message.
        /// </summary>
        public int HandleId { get; private set; }

        /// <summary>
        /// </summary>
        public int OtherHandle { get; private set; }

        /// <summary>
        /// Gets the chats associated with the message.
        /// </summary>
        public ChatEntity[] Chats { get; private set; }

        /// <summary>
        /// Gets the attachments associated with the message.
        /// </summary>
        public AttachmentEntity[] Attachments { get; private set; }

        /// <summary>
        /// Gets the message's subject text.
        /// </summary>
        public string Subject { get; private set; }

        /// <summary>
        /// </summary>
        public string Country { get; private set; }

        /// <summary>
        /// Gets the message's error code.
        /// </summary>
        public int Error { get; private set; }

        /// <summary>
        /// Gets the date and time in UNIX format of when this message was created.
        /// </summary>
        public long DateCreated { get; private set; }

        /// <summary>
        /// Gets the date and time in UNIX format of when this message was read.
        /// </summary>
        public long? DateRead { get; private set; }

        /// <summary>
        /// Gets the date and time in UNIX format of when this message was delivered.
        /// </summary>
        public long? DateDelivered { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the message was sent by the client.
        /// </summary>
        public bool IsFromMe { get; private set; }

        /// <summary>
        /// </summary>
        public bool IsDelayed { get; private set; }

        /// <summary>
        /// </summary>
        public bool IsAutoReply { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the message is a system message.
        /// </summary>
        public bool IsSystemMessage { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the message is a service message.
        /// </summary>
        public bool IsServiceMessage { get; private set; }

        /// <summary>
        /// </summary>
        public bool IsForward { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the message is archived.
        /// </summary>
        public bool IsArchived { get; private set; }

        /// <summary>
        /// </summary>
        public string CacheRoomnames { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the message is an audio message.
        /// </summary>
        public bool IsAudioMessage { get; private set; }

        /// <summary>
        /// </summary>
        public bool HasDdResults { get; private set; }

        /// <summary>
        /// Gets the date and time in UNIX format of when this message was played back.
        /// </summary>
        public long? DatePlayed { get; private set; }

        /// <summary>
        /// </summary>
        public int ItemType { get; private set; }

        /// <summary>
        /// </summary>
        public string GroupTitle { get; private set; }

        /// <summary>
        /// </summary>
        public int GroupActionType { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the message was expired.
        /// </summary>
        public bool IsExpired { get; private set; }

        /// <summary>
        /// </summary>
        public string BalloonBundleId { get; private set; }

        /// <summary>
        /// </summary>
        public string AssociatedMessageGuid { get; private set; }

        /// <summary>
        /// </summary>
        public string AssociatedMessageType { get; private set; }

        /// <summary>
        /// </summary>
        public string ExpressiveSendStyleId { get; private set; }

        /// <summary>
        /// </summary>
        public int? TimeExpressiveSendStyleId { get; private set; }

        /// <summary>
        /// </summary>
        public string ReplyToGuid { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the message is corrupt.
        /// </summary>
        public bool IsCorrupt { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the message is marked as spam.
        /// </summary>
        public bool IsSpam { get; private set; }

        /// <summary>
        /// </summary>
        public string ThreadOriginatorGuid { get; private set; }

        /// <summary>
        /// </summary>
        public string ThreadOriginatorPart { get; private set; }

        /// <summary>
        /// Represents a message's attributed body data.
        /// </summary>
        public sealed class AttributedData
        {
            /// <summary>
            /// Gets the attributed data's text string.
            /// </summary>
            public string String { get; private set; }

            /// <summary>
            /// Gets the list of runs applied to the attributed data.
            /// </summary>
            public Run[] Runs { get; private set; }

            /// <summary>
            /// Represents a section of formatted text.
            /// </summary>
            public sealed class Run
            {
                /// <summary>
                /// Gets the range of the run [X, X].
                /// </summary>
                public int[] Range { get; private set; }

                /// <summary>
                /// Gets the list of attributes applied to the run.
                /// </summary>
                public Dictionary<string, object> Attributes { get; private set; }
            }
        }
    }
}
