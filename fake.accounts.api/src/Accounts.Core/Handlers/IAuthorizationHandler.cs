using System.Threading.Tasks;
using Accounts.Core.DTO.Requests;
using Accounts.Core.DTO.Responses;

namespace Accounts.Core.Handlers
{
    public interface IUserAuthorizationHandler
    {
        Task<AppTokenResponse> RegisterAsync(RegisterRequest request);
        Task<AppTokenResponse> RefrashAsync(RefrashUserRequest request);
        Task<AppTokenResponse> AuthenticationAsync(LoginRequest request);
    }
}