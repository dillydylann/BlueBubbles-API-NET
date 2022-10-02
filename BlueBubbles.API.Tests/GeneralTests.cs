// This file is a part of the BlueBubbles .NET API library
// Copyright (c) 2022 Dylan Briedis <dylan@dylanbriedis.com>

namespace BlueBubbles.API.Tests
{
    [TestClass, TestCategory("General")]
    public sealed class GeneralTests
    {
        private BlueBubblesClient client;

        [TestInitialize]
        public void Init() => client = new BlueBubblesClient(TestConstants.ServerUrl, TestConstants.ServerPassword);

        [TestMethod("GET /api/v1/ping")]
        public void Ping()
        {
            var resp = client.General.Ping();
            TestUtils.VerifyResponse(resp);

            Assert.AreEqual(resp.Data, "pong");
        }
    }
}
