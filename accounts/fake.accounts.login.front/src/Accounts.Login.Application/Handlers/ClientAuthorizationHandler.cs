using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Accounts.Login.Core.Repositories;

namespace Accounts.Login.Application.Handlers
{
    public class ClientAuthorizationHandler : DelegatingHandler
    {
        private readonly IClientAuthorizationRepository _clientAuthorizationRepository;

        public ClientAuthorizationHandler(IClientAuthorizationRepository clientAuthorizationRepository)
        {
            _clientAuthorizationRepository = clientAuthorizationRepository ?? throw new ArgumentNullException(nameof(clientAuthorizationRepository));
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await _clientAuthorizationRepository.GetTokenAsync();

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }
    }
}