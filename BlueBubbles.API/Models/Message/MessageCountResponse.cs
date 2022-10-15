// This file is a part of the BlueBubbles .NET API library
// Copyright (c) 2022 Dylan Briedis <dylan@dylanbriedis.com>

namespace BlueBubbles.API.Models.Message
{
    /// <summary>
    /// Response model for <c>GET /api/v1/message/count</c>,
    /// <c>GET /api/v1/message/count/updated</c>, and
    /// <c>GET /api/v1/message/count/me</c>.
    /// </summary>
    public sealed class MessageCountResponse : BaseCountResponse
    {
    }
}
