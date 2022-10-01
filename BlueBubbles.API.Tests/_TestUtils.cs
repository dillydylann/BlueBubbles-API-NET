// This file is a part of the BlueBubbles .NET API library
// Copyright (c) 2022 Dylan Briedis <dylan@dylanbriedis.com>

using System.Diagnostics;

namespace BlueBubbles.API.Tests
{
    public static class TestUtils
    {
        public static void VerifyResponse<T>(APIResponse<T> resp)
        {
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
    }
}
