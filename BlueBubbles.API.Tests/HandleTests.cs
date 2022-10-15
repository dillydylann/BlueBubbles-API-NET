// This file is a part of the BlueBubbles .NET API library
// Copyright (c) 2022 Dylan Briedis <dylan@dylanbriedis.com>

using BlueBubbles.API.Models.Handle;

namespace BlueBubbles.API.Tests
{
    [TestClass, TestCategory("Handle")]
    public sealed class HandleTests
    {
        private BlueBubblesClient client;

        [TestInitialize]
        public void Init() => client = new BlueBubblesClient(TestConstants.ServerUrl, TestConstants.ServerPassword);

        [TestMethod("GET /api/v1/handle/count")]
        public void Count()
        {
            var resp = client.Handle.Count();
            TestUtils.VerifyResponse(resp);

            Console.WriteLine($"Total handle count: {resp.Data.Total:N0}");
        }

        [TestMethod("POST /api/v1/handle/query")]
        public void Query()
        {
            Assert.IsFalse(string.IsNullOrEmpty(TestConstants.HandleTestAddress));

            var resp = client.Handle.Query(new HandleQueryRequest()
            {
                Limit = 10,
                Offset = 0,
                Address = TestConstants.HandleTestAddress,
                With = { "chat" },
            });
            TestUtils.VerifyResponse(resp);
            TestUtils.PrintQueryMetadata(resp.Metadata);

            Console.WriteLine();
            foreach (var handle in resp.Data)
            {
                PrintHandle(handle);
                Console.WriteLine();
            }
        }

        [TestMethod("GET /api/v1/handle/:guid")]
        public void Find()
        {
            Assert.IsFalse(string.IsNullOrEmpty(TestConstants.HandleTestAddress));

            var resp = client.Handle.Find(TestConstants.HandleTestAddress);
            TestUtils.VerifyResponse(resp);

            PrintHandle(resp.Data);
        }

        public static void PrintHandle(HandleEntity handle)
        {
            Console.WriteLine($"Address: {handle.Address}");
            Console.WriteLine($"Country: {handle.Country}");
            Console.WriteLine($"Uncanonicalized ID: {handle.UncanonicalizedId}");
        }
    }
}
