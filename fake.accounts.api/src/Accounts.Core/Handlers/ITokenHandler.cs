
using Accounts.Core.DTO.Responses;
using Accounts.Core.Entities;
using Accounts.Core.DTO.Requests;

namespace Accounts.Core.Handlers
{
    public interface ITokenHandler
    {
        TokenResponse Create(Client client);
        AppTokenResponse Create(User user, LoginRequest request);
    }
}