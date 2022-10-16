# BlueBubbles API for .NET

A client library for sending and receiving iMessages from and to a [BlueBubbles](https://bluebubbles.app/) server.

## Missing Features

- Google FCM device registration
- iCloud Find My support
- Socket.IO events (for notifications)

## Usage

### Setup

1. [Install the BlueBubbles server on a Mac.](https://github.com/BlueBubblesApp/bluebubbles-server/releases)

2. Instantiate the client with your server URL and password:

```csharp
using BlueBubbles.API;

var client = new BlueBubblesClient("http://myserver:1234/", "password");
```

### Examples

#### Attachment API

##### Download an attachment

```csharp
using System.IO;
using BlueBubbles.API.Models.Attachment;

// Grab the attachment from a message entity that has one
// (replace this with wherever you got your attachment object)
var attachment = message.Attachments[0];

// Downloads and saves it to a file
using var stream = await client.Attachment.DownloadAsync(attachment.Guid);
using var file = File.Create(attachment.TransferName);

stream.CopyTo(file);
```

#### Chat API

##### Create a new chat conversation with a message

```csharp
using BlueBubbles.API.Models.Chat;

// Creates a new chat with a phone number and message
var response = await client.Chat.CreateAsync(new ChatCreateRequest(new[] { "+18005551234" }, "Hello world!"));
var chat = response.Data; // Your newly created chat
```

##### Query chat conversations

```csharp
using BlueBubbles.API.Models.Chat;

var response = await client.Chat.QueryAsync(new ChatQueryRequest()
{
    Limit = 10,                         // Limits the amount of results to 10
    Offset = 0,                         // Pagination offset by 0
    Sort = "lastmessage",               // Sorts by the last message sent
    With = { "lastMessage", "sms" },    // Populate the last message sent in the chat
});
var chats = response.Data;

// Loops through chats and prints the participants and last message
foreach (var chat in chats)
{
    Console.WriteLine($"Addresses: {string.Join(", ", chat.Participants.Select(h => h.Address))}");
    Console.WriteLine($"Last message: {chat.LastMessage.Text}");
    Console.WriteLine();
}
```

##### Fetch messages from a chat conversation

```csharp
using BlueBubbles.API.Models.Message;

// You can specify how many messages you want with limit and offset
var response = await client.Chat.GetMessagesAsync(chat.Guid, limit: 10, offset: 0);
var messages = response.Data;

// Loops through each message and prints the text
foreach (var message in messages)
{
    Console.WriteLine(message.Text);
}
```

#### Contact API

##### Fetch all contacts from the server

```csharp
using BlueBubbles.API.Models.Contact;

var response = await client.Contact.GetAsync();
var contacts = response.Data;

// Loops through contacts and sorts them by first name
foreach (var contact in contacts.OrderBy(c => c.FirstName))
{
    // Prints the first and last name of the contact
    Console.WriteLine($"-- {contact.FirstName} {contact.LastName} --");

    // Loops through and prints any phone numbers and emails
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
```

#### Handle API

##### Query handles by address

```csharp
using BlueBubbles.API.Models.Handle;

var response = await client.Handle.QueryAsync(new HandleQueryRequest()
{
    Limit = 10,                 // Limits the amount of results to 10
    Offset = 0,                 // Pagination offset by 0
    Address = "+18005551234",   // The address to query
    With = { "chat" },          // Populate the chats the handle is in
});
var handles = response.Data;

// Loops through handles and prints their address (phone number or email) and country
foreach (var handle in handles)
{
    Console.WriteLine($"Address: {handle.Address}");
    Console.WriteLine($"Country: {handle.Country}");
    Console.WriteLine();
}
```

#### Message API

##### Send a text message to a chat conversation

```csharp
using BlueBubbles.API.Models.Message;

var response = client.Message.SendText(new MessageTextRequest(chat.Guid, "Hello world!"));
var message = response.Data; // Your newly created text message
```

##### Send an attachment to a chat conversation

```csharp
using BlueBubbles.API.Models.Message;

// Opens the file and sends it to the chat
var fileInfo = new FileInfo(@"D:\Pictures\test.png");
using (var fileStream = fileInfo.OpenRead())
{
    var response = client.Message.SendAttachment(new MessageAttachmentRequest(chat.Guid, fileInfo.Name, fileStream));
    var message = response.Data; // Your newly created attachment message
}
```

##### React (tapback) to a text message (Private API)

```csharp
using BlueBubbles.API.Models.Message;

// Sends a like (thumbs up) tapback to a text message
var response = client.Message.React(new MessageReactRequest(chat.Guid, message.Guid, MessageReaction.AddLike));
```

#### Server API

##### Fetch server information

```csharp
using BlueBubbles.API.Models.Server;

var response = await client.Server.GetInfoAsync();
var serverInfo = response.Data;

Console.WriteLine($"OS version: {serverInfo.OSVersion}");
Console.WriteLine($"Server version: {serverInfo.ServerVersion}");
Console.WriteLine($"Is Private API enabled:{serverInfo.PrivateAPIEnabled}");
Console.WriteLine($"Proxy service: {serverInfo.ProxyService}");
Console.WriteLine($"Is Private API helper connected:{serverInfo.HelperConnected}");
Console.WriteLine($"Detected iCloud Email: {serverInfo.DetectediCloudEmail}");
```
