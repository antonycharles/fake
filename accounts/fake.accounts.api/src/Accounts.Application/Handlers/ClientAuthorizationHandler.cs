using Accounts.Application.Exceptions;
using Accounts.Core.DTO.Requests;
using Accounts.Core.DTO.Responses;
using Accounts.Core.Handlers;
using Accounts.Core.Providers;
using Accounts.Core.Repositories;

namespace Accounts.Application.Handlers
{
    public class ClientAuthorizationHandler : IClientAuthorizationHandler
    {
        private const string MSG_CLIENT_OR_SECRET_INVALID = "Client id or secret invalid";
        private readonly IClientRepository _clientRepository;
        private readonly IPasswordProvider _passwordProvider;
        private readonly ITokenHandler _tokenHandler;

        public ClientAuthorizationHandler(
            IClientRepository clientRepository, 
            IPasswordProvider passwordProvider, 
            ITokenHandler tokenHandler)
        {
            _clientRepository = clientRepository ?? throw new ArgumentNullException(nameof(clientRepository));
            _passwordProvider = passwordProvider ?? throw new ArgumentNullException(nameof(passwordProvider));
            _tokenHandler = tokenHandler ?? throw new ArgumentNullException(nameof(tokenHandler));
        }

        public async Task<TokenResponse> AuthenticationAsync(ClientLoginRequest request)
        {
            var clientDb = await _clientRepository.GetByIdAsync<int>(request.ClientId);
            
            if(clientDb == null)
                throw new AuthenticationException(MSG_CLIENT_OR_SECRET_INVALID);

            var passwordHash = _passwordProvider.HashPassword(request.ClientSecret, clientDb.Salt);

            if(clientDb.SecretHash != passwordHash)
                throw new AuthenticationException(MSG_CLIENT_OR_SECRET_INVALID);
                

            return _tokenHandler.Create(clientDb);
        }
    }
}