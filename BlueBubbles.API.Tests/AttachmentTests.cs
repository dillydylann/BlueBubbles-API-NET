// This file is a part of the BlueBubbles .NET API library
// Copyright (c) 2022 Dylan Briedis <dylan@dylanbriedis.com>

namespace BlueBubbles.API.Tests
{
    [TestClass, TestCategory("Attachment")]
    public sealed class AttachmentTests
    {
        private BlueBubblesClient client;

        [TestInitialize]
        public void Init() => client = new BlueBubblesClient(TestConstants.ServerUrl, TestConstants.ServerPassword);

        [TestMethod("GET /api/v1/attachment/count")]
        public void Count()
        {
            var resp = client.Attachment.Count();
            TestUtils.VerifyResponse(resp);

            Console.WriteLine($"Total attachment count: {resp.Data.Total:N0}");
        }

        [TestMethod("GET /api/v1/attachment/:guid/download")]
        public void Download()
        {
            Assert.IsFalse(string.IsNullOrEmpty(TestConstants.AttachmentTestGuid));

            // Find() will have to succeed first!
            var resp = client.Attachment.Find(TestConstants.AttachmentTestGuid);
            TestUtils.VerifyResponse(resp);

            using var stream = client.Attachment.Download(TestConstants.AttachmentTestGuid);
            using var file = File.Create(resp.Data.TransferName);

            stream.CopyTo(file);
            Console.WriteLine("File is saved as " + file.Name);
        }

        [TestMethod("GET /api/v1/attachment/:guid/blurhash")]
        public void BlurHash()
        {
            Assert.IsFalse(string.IsNullOrEmpty(TestConstants.AttachmentTestGuid));

            var resp = client.Attachment.BlurHash(TestConstants.AttachmentTestGuid);
            TestUtils.VerifyResponse(resp);

            Console.WriteLine($"BlurHash: {resp.Data}");
        }

        [TestMethod("GET /api/v1/attachment/:guid")]
        public void Find()
        {
            Assert.IsFalse(string.IsNullOrEmpty(TestConstants.AttachmentTestGuid));

            var resp = client.Attachment.Find(TestConstants.AttachmentTestGuid);
            TestUtils.VerifyResponse(resp);

            Console.WriteLine($"GUID: {resp.Data.Guid}");
            Console.WriteLine($"Width: {resp.Data.Width}");
            Console.WriteLine($"Height: {resp.Data.Height}");
            Console.WriteLine($"UTI: {resp.Data.Uti}");
            Console.WriteLine($"MIME type: {resp.Data.MimeType}");
            Console.WriteLine($"Transfer name: {resp.Data.TransferName}");
            Console.WriteLine($"Total bytes: {resp.Data.TotalBytes}");
        }
    }
}
