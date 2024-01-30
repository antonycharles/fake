using Accounts.Login.Core.Models.Login;
using Accounts.Login.Core.Models.Register;
using Accounts.Login.Core.Models.Token;

namespace Accounts.Login.Core.Handlers.Interfaces
{
    public interface ILoginHandler
    {
        Task<AppTokenResponse> AuthenticationAsync(LoginRequest request);
        Task<AppTokenResponse> RegisterAsync(RegisterRequest request);
    }
}