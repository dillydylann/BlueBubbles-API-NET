// This file is a part of the BlueBubbles .NET API library
// Copyright (c) 2022 Dylan Briedis <dylan@dylanbriedis.com>

using System;

namespace BlueBubbles.API.Models
{
    /// <summary>
    /// Marks a method as part of the Private API.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class PrivateAPIAttribute : Attribute
    {
    }
}
