// This file is a part of the BlueBubbles .NET API library
// Copyright (c) 2022 Dylan Briedis <dylan@dylanbriedis.com>

#pragma warning disable SYSLIB0014

using BlueBubbles.API.Models.Message;

namespace BlueBubbles.API.Tests
{
    [TestClass, TestCategory("Message")]
    public sealed class MessageTests
    {
        private BlueBubblesClient client;

        [TestInitialize]
        public void Init() => client = new BlueBubblesClient(TestConstants.ServerUrl, TestConstants.ServerPassword);

        [TestMethod("GET /api/v1/message/count")]
        public void Count()
        {
            var resp = client.Message.Count();
            TestUtils.VerifyResponse(resp);

            Console.WriteLine($"Total message count: {resp.Data.Total}");
        }

        [TestMethod("GET /api/v1/message/count/updated")]
        public void UpdatedCount()
        {
            var resp = client.Message.UpdatedCount(0);
            TestUtils.VerifyResponse(resp);

            Console.WriteLine($"Total message count: {resp.Data.Total}");
        }

        [TestMethod("GET /api/v1/message/count/me")]
        public void SentCount()
        {
            var resp = client.Message.SentCount();
            TestUtils.VerifyResponse(resp);

            Console.WriteLine($"Total message count: {resp.Data.Total}");
        }

        [TestMethod("POST /api/v1/message/text (AppleScript)")]
        public void SendText_AppleScript()
        {
            Assert.IsFalse(string.IsNullOrEmpty(TestConstants.MessageTestChatGuid));

            var resp = client.Message.SendText(new MessageTextRequest(TestConstants.MessageTestChatGuid, "This is a message sent using AppleScript"));
            TestUtils.VerifyResponse(resp);

            PrintMessage(resp.Data);
        }


        [TestMethod("POST /api/v1/message/text (Private API)")]
        [TestCategory("Private API")]
        public void SendText_PrivateAPI()
        {
            Assert.IsFalse(string.IsNullOrEmpty(TestConstants.MessageTestChatGuid));

            var resp = client.Message.SendText(new MessageTextRequest(TestConstants.MessageTestChatGuid, "This is a message sent using the Private API")
            {
                Subject = "Subject test",
            });
            TestUtils.VerifyResponse(resp);

            PrintMessage(resp.Data);
        }

        [TestMethod("POST /api/v1/message/attachment")]
        public void SendAttachment()
        {
            Assert.IsFalse(string.IsNullOrEmpty(TestConstants.MessageTestChatGuid));

            using var stream = WebRequest.CreateHttp("https://placehold.jp/800x600.png").GetResponse().GetResponseStream();
            var resp = client.Message.SendAttachment(new MessageAttachmentRequest(TestConstants.MessageTestChatGuid, "placeholder.png", stream));
            TestUtils.VerifyResponse(resp);

            PrintMessage(resp.Data);
        }

        [TestMethod("POST /api/v1/message/react")]
        [TestCategory("Private API")]
        public void React()
        {
            Assert.IsFalse(string.IsNullOrEmpty(TestConstants.MessageTestChatGuid));
            Assert.IsFalse(string.IsNullOrEmpty(TestConstants.MessageTestGuid));

            var resp = client.Message.React(new MessageReactRequest(TestConstants.MessageTestChatGuid, TestConstants.MessageTestGuid, MessageReaction.AddLike));
            TestUtils.VerifyResponse(resp);
        }

        [TestMethod("GET /api/v1/message/:guid")]
        public void Find()
        {
            Assert.IsFalse(string.IsNullOrEmpty(TestConstants.MessageTestGuid));

            var resp = client.Message.Find(TestConstants.MessageTestGuid);
            TestUtils.VerifyResponse(resp);

            PrintMessage(resp.Data);
        }

        public static void PrintMessage(MessageEntity message)
        {
            Console.WriteLine($"GUID: {message.Guid}");
            Console.WriteLine($"Subject text: {message.Subject}");
            Console.WriteLine($"Message text: {message.Text}");
            Console.WriteLine($"Handle ID: {message.HandleId}");
            Console.WriteLine($"Country: {message.Country}");
            Console.WriteLine($"Error code: {message.Error}");
            Console.WriteLine($"Date created: {message.DateCreated}");
            Console.WriteLine($"Date read: {message.DateRead}");
            Console.WriteLine($"Date delivered: {message.DateDelivered}");
        }
    }
}
