using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Accounts.Login.Core.Handlers;
using Accounts.Login.Core.Repositories;

namespace Accounts.Login.Application.Handlers
{
    public class ClientAuthorizationDelegatingHandler : DelegatingHandler
    {
        private readonly IClientAuthorizationHandler _clientAuthorizationHandler;

        public ClientAuthorizationDelegatingHandler(IClientAuthorizationHandler clientAuthorizationHandler)
        {
            _clientAuthorizationHandler = clientAuthorizationHandler ?? throw new ArgumentNullException(nameof(clientAuthorizationHandler));
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await _clientAuthorizationHandler.GetTokenAsync();

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }
    }
}