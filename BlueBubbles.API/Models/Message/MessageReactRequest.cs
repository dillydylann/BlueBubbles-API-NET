// This file is a part of the BlueBubbles .NET API library
// Copyright (c) 2022 Dylan Briedis <dylan@dylanbriedis.com>

using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BlueBubbles.API.Models.Message
{
    /// <summary>
    /// Defines the message tapback reactions.
    /// </summary>
    /// <seealso href="https://github.com/BlueBubblesApp/bluebubbles-server/blob/9572e21180c95587b45dd3cbe4f7568a0bf47661/packages/server/src/server/types.ts#L107"/>
    public enum MessageReaction
    {
        /// <summary>
        /// Adds a love (heart) tapback.
        /// </summary>
        [EnumMember(Value = "love")]
        AddLove,

        /// <summary>
        /// Adds a like (thumbs up) tapback.
        /// </summary>
        [EnumMember(Value = "like")]
        AddLike,

        /// <summary>
        /// Adds a dislike (thumbs down) tapback.
        /// </summary>
        [EnumMember(Value = "dislike")]
        AddDislike,

        /// <summary>
        /// Adds a laugh (ha ha) tapback.
        /// </summary>
        [EnumMember(Value = "laugh")]
        AddLaugh,

        /// <summary>
        /// Adds a emphasize (!!) tapback.
        /// </summary>
        [EnumMember(Value = "emphasize")]
        AddEmphasize,

        /// <summary>
        /// Adds a question (?) tapback.
        /// </summary>
        [EnumMember(Value = "question")]
        AddQuestion,

        /// <summary>
        /// Removes a love (heart) tapback.
        /// </summary>
        [EnumMember(Value = "-love")]
        RemoveLove,

        /// <summary>
        /// Removes a like (thumbs up) tapback.
        /// </summary>
        [EnumMember(Value = "-like")]
        RemoveLike,

        /// <summary>
        /// Removes a dislike (thumbs down) tapback.
        /// </summary>
        [EnumMember(Value = "-dislike")]
        RemoveDislike,

        /// <summary>
        /// Removes a laugh (ha ha) tapback.
        /// </summary>
        [EnumMember(Value = "-laugh")]
        RemoveLaugh,

        /// <summary>
        /// Removes a emphasize (!!) tapback.
        /// </summary>
        [EnumMember(Value = "-emphasize")]
        RemoveEmphasize,

        /// <summary>
        /// Removes a question (?) tapback.
        /// </summary>
        [EnumMember(Value = "-question")]
        RemoveQuestion,
    }

    /// <summary>
    /// Request model for <c>POST /api/v1/message/react</c>.
    /// </summary>
    public sealed class MessageReactRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MessageReactRequest"/> class.
        /// </summary>
        /// <param name="chatGuid">The GUID for the chat you want this message to be sent to.</param>
        /// <param name="selectedMessageGuid">The message GUID to react to.</param>
        /// <param name="reaction">The reaction to send.</param>
        public MessageReactRequest(string chatGuid, string selectedMessageGuid, MessageReaction reaction)
        {
            ChatGuid = chatGuid ?? throw new ArgumentNullException(nameof(chatGuid));
            SelectedMessageGuid = selectedMessageGuid ?? throw new ArgumentNullException(nameof(selectedMessageGuid));
            Reaction = reaction;
        }

        /// <summary>
        /// Gets or sets the GUID for the chat you want this reaction to be sent to.
        /// </summary>
        public string ChatGuid { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the message GUID to react to.
        /// </summary>
        public string SelectedMessageGuid { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the reaction to send.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public MessageReaction Reaction { get; set; } = 0;
    }
}
