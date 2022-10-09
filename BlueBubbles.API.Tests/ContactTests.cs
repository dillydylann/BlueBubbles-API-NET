// This file is a part of the BlueBubbles .NET API library
// Copyright (c) 2022 Dylan Briedis <dylan@dylanbriedis.com>

using BlueBubbles.API.Models.Contact;

namespace BlueBubbles.API.Tests
{
    [TestClass, TestCategory("Contact")]
    public sealed class ContactTests
    {
        private BlueBubblesClient client;

        [TestInitialize]
        public void Init() => client = new BlueBubblesClient(TestConstants.ServerUrl, TestConstants.ServerPassword);

        [TestMethod("GET /api/v1/contact")]
        public void Get()
        {
            var resp = client.Contact.Get();
            TestUtils.VerifyResponse(resp);

            PrintContacts(resp.Data);
        }

        [TestMethod("POST /api/v1/contact")]
        public void Create()
        {
            const string firstName = "Johnny", lastName = "Appleseed";

            var resp = client.Contact.Create(new ContactCreateRequest(firstName, lastName));
            TestUtils.VerifyResponse(resp);

            PrintContacts(resp.Data);

            Assert.AreEqual(resp.Data[0].FirstName, firstName);
            Assert.AreEqual(resp.Data[0].LastName, lastName);
        }

        [TestMethod("POST /api/v1/contact/query")]
        public void Query()
        {
            Assert.IsTrue(TestConstants.ContactQueryTestAddresses.Length > 0);

            var resp = client.Contact.Query(new ContactQueryRequest(TestConstants.ContactQueryTestAddresses));
            TestUtils.VerifyResponse(resp);

            PrintContacts(resp.Data);
        }

        private void PrintContacts(IEnumerable<ContactObject> contacts)
        {
            Console.WriteLine();
            foreach (var contact in contacts.OrderBy(c => c.FirstName))
            {
                Console.WriteLine($"-- {contact.FirstName} {contact.LastName} --");
                Console.WriteLine($"ID: {contact.Id}");
                Console.WriteLine($"Source type: {contact.SourceType}");
                if (!string.IsNullOrEmpty(contact.DisplayName))
                {
                    Console.WriteLine($"Display name: {contact.DisplayName}");
                }
                if (DateOnly.TryParseExact(contact.Birthday, "yyyy-MM-dd", out DateOnly date))
                {
                    Console.WriteLine($"Birthday: {date}");
                }
                foreach (var phoneNumber in contact.PhoneNumbers)
                {
                    Console.WriteLine($"Phone number: {phoneNumber.Address}");
                }
                foreach (var email in contact.Emails)
                {
                    Console.WriteLine($"Email: {email.Address}");
                }
                Console.WriteLine();
            }
        }
    }
}
