// This file is a part of the BlueBubbles .NET API library
// Copyright (c) 2022 Dylan Briedis <dylan@dylanbriedis.com>

using BlueBubbles.API.Models.Server;

namespace BlueBubbles.API.Tests
{
    [TestClass, TestCategory("Server")]
    public sealed class ServerTests
    {
        private BlueBubblesClient client;

        [TestInitialize]
        public void Init() => client = new BlueBubblesClient(TestConstants.ServerUrl, TestConstants.ServerPassword);

        [TestMethod("GET /api/v1/server/info")]
        public void GetInfo()
        {
            var resp = client.Server.GetInfo();
            TestUtils.VerifyResponse(resp);

            Console.WriteLine($"OS version: {resp.Data.OSVersion}");
            Console.WriteLine($"Server version: {resp.Data.ServerVersion}");
            Console.WriteLine($"Is Private API enabled: {resp.Data.PrivateAPIEnabled}");
            Console.WriteLine($"Proxy service: {resp.Data.ProxyService}");
            Console.WriteLine($"Is Private API helper connected: {resp.Data.HelperConnected}");
            Console.WriteLine($"Detected iCloud Email: {resp.Data.DetectediCloudEmail}");
        }

        [TestMethod("GET /api/v1/server/logs")]
        public void GetLogs()
        {
            var resp = client.Server.GetLogs();
            TestUtils.VerifyResponse(resp);

            Console.WriteLine();
            Console.WriteLine(resp.Data);
        }

        [TestMethod("GET /api/v1/server/restart/soft")]
        [Ignore("Not normally enabled because restarting the server while testing could lead to problems.")]
        public void RestartServices()
        {
            var resp = client.Server.RestartServices();
            TestUtils.VerifyResponse(resp);
        }

        [TestMethod("GET /api/v1/server/restart/hard")]
        [Ignore("Not normally enabled because restarting the server while testing could lead to problems.")]
        public void RestartAll()
        {
            var resp = client.Server.RestartAll();
            TestUtils.VerifyResponse(resp);
        }

        [TestMethod("GET /api/v1/server/alert")]
        public void GetAlerts()
        {
            var resp = client.Server.GetAlerts();
            TestUtils.VerifyResponse(resp);

            Console.WriteLine();
            foreach (var alert in resp.Data)
            {
                Console.WriteLine($"ID: {alert.Id}");
                Console.WriteLine($"Type: {alert.Type}");
                Console.WriteLine($"Message: {alert.Value}");
                Console.WriteLine($"Is read: {alert.IsRead}");
                Console.WriteLine($"Created: {alert.Created}");
                Console.WriteLine($"Updated: {alert.Updated}");
                Console.WriteLine();
            }
        }

        [TestMethod("POST /api/v1/server/alert/read")]
        public void MarkAlertsAsRead()
        {
            var resp = client.Server.MarkAlertsAsRead(new ServerMarkAlertsAsReadRequest(new[] { 1, 2, 3, 4, 5 }));
            TestUtils.VerifyResponse(resp);
        }

        [TestMethod("GET /api/v1/server/statistics/totals")]
        public void GetStatTotals()
        {
            var resp = client.Server.GetStatTotals();
            TestUtils.VerifyResponse(resp);

            Console.WriteLine($"Handles: {resp.Data.Handles:N0}");
            Console.WriteLine($"Messages: {resp.Data.Messages:N0}");
            Console.WriteLine($"Chats: {resp.Data.Chats:N0}");
            Console.WriteLine($"Attachments: {resp.Data.Attachments:N0}");
            Console.WriteLine($"Total: {resp.Data.Total:N0}");
        }

        [TestMethod("GET /api/v1/server/statistics/media")]
        public void GetStatMedia()
        {
            var resp = client.Server.GetStatMedia();
            TestUtils.VerifyResponse(resp);

            Console.WriteLine($"Images: {resp.Data.Images:N0}");
            Console.WriteLine($"Videos: {resp.Data.Videos:N0}");
            Console.WriteLine($"Locations: {resp.Data.Locations:N0}");
            Console.WriteLine($"Total: {resp.Data.Total:N0}");
        }

        [TestMethod("GET /api/v1/server/statistics/media/chat")]
        public void GetStatMediaByChat()
        {
            var resp = client.Server.GetStatMediaByChat();
            TestUtils.VerifyResponse(resp);

            Console.WriteLine();
            foreach (var totals in resp.Data)
            {
                Console.WriteLine($"Chat GUID: {totals.ChatGuid}");
                if (!string.IsNullOrEmpty(totals.GroupName))
                {
                    Console.WriteLine($"Group name: {totals.GroupName}");
                }
                Console.WriteLine($"Images: {totals.Totals.Images:N0}");
                Console.WriteLine($"Videos: {totals.Totals.Videos:N0}");
                Console.WriteLine($"Locations: {totals.Totals.Locations:N0}");
                Console.WriteLine($"Total: {totals.Totals.Total:N0}");
                Console.WriteLine();
            }
        }
    }
}
