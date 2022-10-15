// This file is a part of the BlueBubbles .NET API library
// Copyright (c) 2022 Dylan Briedis <dylan@dylanbriedis.com>

using System.Diagnostics;
using BlueBubbles.API.Models;

namespace BlueBubbles.API.Tests
{
    public static class TestUtils
    {
        public static void VerifyResponse(APIResponse resp)
        {
            // Ehh...a normal user would be using PrivateAPIEnabled and HelperConnected in
            // Server.GetInfo() instead of this mess...
            if (resp.Error?.Message == "iMessage Private API is not enabled!" ||
                resp.Error?.Message == "iMessage Private API Helper is not connected!")
            {
                Assert.Inconclusive("The Private API is either disabled or its helper is not connected.");
            }

            Console.WriteLine($"Response status: {resp.Status}");
            Console.WriteLine($"Response message: {resp.Message}");
            if (resp.Error != null)
            {
                Console.WriteLine($"Response error type: {resp.Error.Type}");
                Console.WriteLine($"Response error message: {resp.Error.Message}");
            }
            Debug.WriteLine($"Raw JSON: {resp.RawJson}");

            Assert.AreEqual(resp.Status, HttpStatusCode.OK);
        }

        public static void PrintQueryMetadata(QueryMetadata metadata)
        {
            Console.WriteLine($"Query limit: {metadata.Limit}");
            Console.WriteLine($"Query offset: {metadata.Offset}");
            Console.WriteLine($"Query total: {metadata.Total}");
        }
    }
}
