// This file is a part of the BlueBubbles .NET API library
// Copyright (c) 2022 Dylan Briedis <dylan@dylanbriedis.com>

using BlueBubbles.API.Models.Chat;

namespace BlueBubbles.API.Tests
{
    [TestClass, TestCategory("Chat")]
    public sealed class ChatTests
    {
        private BlueBubblesClient client;

        [TestInitialize]
        public void Init() => client = new BlueBubblesClient(TestConstants.ServerUrl, TestConstants.ServerPassword);

        [TestMethod("POST /api/v1/chat/new")]
        public void Create()
        {
            Assert.IsFalse(string.IsNullOrEmpty(TestConstants.ChatCreateTestAddress));

            var resp = client.Chat.Create(new ChatCreateRequest(new[] { TestConstants.ChatCreateTestAddress }, "This is a test message!"));
            TestUtils.VerifyResponse(resp);

            PrintChat(resp.Data);
        }

        [TestMethod("GET /api/v1/chat/count")]
        public void Count()
        {
            var resp = client.Chat.Count();
            TestUtils.VerifyResponse(resp);

            Console.WriteLine($"Total chat count: {resp.Data.Total:N0}");
            Console.WriteLine($"iMessage count: {resp.Data.Breakdown.iMessage:N0}");
            Console.WriteLine($"SMS count: {resp.Data.Breakdown.SMS:N0}");
        }

        [TestMethod("POST /api/v1/chat/query")]
        public void Query()
        {
            var resp = client.Chat.Query(new ChatQueryRequest()
            {
                Limit = 10,
                Offset = 0,
                Sort = "lastmessage",
                With = { "lastMessage", "sms" },
            });
            TestUtils.VerifyResponse(resp);
            TestUtils.PrintQueryMetadata(resp.Metadata);

            Console.WriteLine();
            foreach (var chat in resp.Data)
            {
                PrintChat(chat);
                Console.WriteLine();
            }
        }

        [TestMethod("GET /api/v1/chat/:guid/message")]
        public void GetMessages()
        {
            Assert.IsFalse(string.IsNullOrEmpty(TestConstants.ChatTestGuid));

            var resp = client.Chat.GetMessages(TestConstants.ChatTestGuid);
            TestUtils.VerifyResponse(resp);

            Console.WriteLine();
            foreach (var message in resp.Data)
            {
                PrintMessage(message);
                Console.WriteLine();
            }
        }

        [TestMethod("POST /api/v1/chat/:guid/read")]
        [TestCategory("Private API")]
        public void MarkRead()
        {
            Assert.IsFalse(string.IsNullOrEmpty(TestConstants.ChatTestGuid));

            var resp = client.Chat.MarkRead(TestConstants.ChatTestGuid);
            TestUtils.VerifyResponse(resp);
        }

        [TestMethod("GET /api/v1/chat/:guid/icon")]
        public void GetGroupIcon()
        {
            Assert.IsFalse(string.IsNullOrEmpty(TestConstants.ChatTestGuid));

            using var stream = client.Chat.GetGroupIcon(TestConstants.ChatTestGuid);
            using var file = File.Create("groupicon.png");

            stream.CopyTo(file);
            Console.WriteLine("File is saved as " + file.Name);
        }

        [TestMethod("GET /api/v1/chat/:guid")]
        public void Find()
        {
            Assert.IsFalse(string.IsNullOrEmpty(TestConstants.ChatTestGuid));

            var resp = client.Chat.Find(TestConstants.ChatTestGuid);
            TestUtils.VerifyResponse(resp);

            PrintChat(resp.Data);
        }

        [TestMethod("PUT /api/v1/chat/:guid")]
        public void Update()
        {
            Assert.IsFalse(string.IsNullOrEmpty(TestConstants.ChatTestGuid));

            var resp = client.Chat.Update(TestConstants.ChatTestGuid, new ChatUpdateRequest("Chat rename test!"));
            TestUtils.VerifyResponse(resp);

            PrintChat(resp.Data);
        }

        [TestMethod("DELETE /api/v1/chat/:guid")]
        [TestCategory("Private API")]
        [Ignore("Not normally enabled since this can be destructive!")]
        public void Delete()
        {
            Assert.IsFalse(string.IsNullOrEmpty(TestConstants.ChatTestGuid));

            var resp = client.Chat.Delete(TestConstants.ChatTestGuid);
            TestUtils.VerifyResponse(resp);
        }

        public static void PrintChat(ChatEntity chat)
        {
            Console.WriteLine($"GUID: {chat.Guid}");
            if (chat.LastMessage != null)
            {
                Console.WriteLine($"Last message: {chat.LastMessage.Text}");
            }
            Console.WriteLine($"Style: {chat.Style}");
            Console.WriteLine($"Chat identifier: {chat.ChatIdentifier}");
            Console.WriteLine($"Is archived: {chat.IsArchived}");
            Console.WriteLine($"Is filtered: {chat.IsFiltered}");
            if (!string.IsNullOrEmpty(chat.DisplayName))
            {
                Console.WriteLine($"Display name: {chat.DisplayName}");
            }
            Console.WriteLine($"Group ID: {chat.GroupId}");
        }
    }
}
