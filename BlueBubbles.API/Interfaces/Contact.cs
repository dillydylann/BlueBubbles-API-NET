// This file is a part of the BlueBubbles .NET API library
// Copyright (c) 2022 Dylan Briedis <dylan@dylanbriedis.com>

using System.Threading.Tasks;
using BlueBubbles.API.Models.Contact;

namespace BlueBubbles.API.Interfaces
{
    /// <summary>
    /// Contains methods for the Contact API interface.
    /// </summary>
    public interface IContact
    {
        /// <summary>
        /// Fetches contacts from the server.
        /// </summary>
        /// <remarks>
        /// Requests with <c>GET /api/v1/contact</c>.
        /// </remarks>
        /// <returns>A response with an array of contacts.</returns>
        APIResponse<ContactObject[]> Get();

        /// <inheritdoc cref="Get"/>
        Task<APIResponse<ContactObject[]>> GetAsync();

        /// <summary>
        /// Creates a contact on the server.
        /// </summary>
        /// <remarks>
        /// Requests with <c>POST /api/v1/contact</c>.
        /// </remarks>
        /// <param name="request">The parameters for this request.</param>
        /// <returns>A response with an array of contacts created.</returns>
        APIResponse<ContactObject[]> Create(ContactCreateRequest request);

        /// <inheritdoc cref="Create"/>
        Task<APIResponse<ContactObject[]>> CreateAsync(ContactCreateRequest request);

        /// <summary>
        /// Queries contacts from the server.
        /// </summary>
        /// <remarks>
        /// Requests with <c>POST /api/v1/contact/query</c>.
        /// </remarks>
        /// <param name="request">The parameters for this request.</param>
        /// <returns>A response with an array of contacts.</returns>
        APIResponse<ContactObject[]> Query(ContactQueryRequest request);

        /// <inheritdoc cref="Query"/>
        Task<APIResponse<ContactObject[]>> QueryAsync(ContactQueryRequest request);
    }

#pragma warning disable SA1600
    internal sealed class ContactImpl : IContact
    {
        public const string GetOrCreateUrlPath = "/api/v1/contact";
        public const string QueryUrlPath = "/api/v1/contact/query";

        private BlueBubblesClient client;
        public ContactImpl(BlueBubblesClient c) => client = c;

        public APIResponse<ContactObject[]> Get()
            => client.RequestGet<ContactObject[]>(GetOrCreateUrlPath);
        public Task<APIResponse<ContactObject[]>> GetAsync()
            => client.RequestGetAsync<ContactObject[]>(GetOrCreateUrlPath);

        public APIResponse<ContactObject[]> Create(ContactCreateRequest request)
            => client.RequestPost<ContactObject[], ContactCreateRequest>(GetOrCreateUrlPath, request);
        public Task<APIResponse<ContactObject[]>> CreateAsync(ContactCreateRequest request)
            => client.RequestPostAsync<ContactObject[], ContactCreateRequest>(GetOrCreateUrlPath, request);

        public APIResponse<ContactObject[]> Query(ContactQueryRequest request)
            => client.RequestPost<ContactObject[], ContactQueryRequest>(QueryUrlPath, request);
        public Task<APIResponse<ContactObject[]>> QueryAsync(ContactQueryRequest request)
            => client.RequestPostAsync<ContactObject[], ContactQueryRequest>(QueryUrlPath, request);
    }
#pragma warning restore SA1600
}
