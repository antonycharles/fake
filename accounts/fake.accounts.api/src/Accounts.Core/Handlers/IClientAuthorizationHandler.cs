
using Accounts.Core.DTO.Requests;
using Accounts.Core.DTO.Responses;

namespace Accounts.Core.Handlers
{
    public interface IClientAuthorizationHandler
    {
        Task<TokenResponse> AuthenticationAsync(ClientLoginRequest request);
    }
}