// This file is a part of the BlueBubbles .NET API library
// Copyright (c) 2022 Dylan Briedis <dylan@dylanbriedis.com>

namespace BlueBubbles.API.Tests
{
    [TestClass, TestCategory("Mac")]
    public sealed class MacOSTests
    {
        private BlueBubblesClient client;

        [TestInitialize]
        public void Init() => client = new BlueBubblesClient(TestConstants.ServerUrl, TestConstants.ServerPassword);

        [TestMethod("POST /api/v1/mac/lock")]
        [Ignore("Not normally enabled since we don't want to lock out the server.")]
        public void Lock()
        {
            var resp = client.MacOS.Lock();
            TestUtils.VerifyResponse(resp);
        }

        [TestMethod("POST /api/v1/mac/imessage/restart")]
        public void RestartMessagesApp()
        {
            var resp = client.MacOS.RestartMessagesApp();
            TestUtils.VerifyResponse(resp);
        }
    }
}
