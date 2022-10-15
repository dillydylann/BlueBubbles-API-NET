// This file is a part of the BlueBubbles .NET API library
// Copyright (c) 2022 Dylan Briedis <dylan@dylanbriedis.com>

namespace BlueBubbles.API.Models
{
    /// <summary>
    /// Represents the base class for response models with a total number of entities.
    /// </summary>
    public class BaseCountResponse
    {
        /// <summary>
        /// Gets the total number of entities that were returned.
        /// </summary>
        public int Total { get; private set; }
    }
}
