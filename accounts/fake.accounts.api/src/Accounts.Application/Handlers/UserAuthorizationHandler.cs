using Accounts.Application.Exceptions;
using Accounts.Core.DTO.Requests;
using Accounts.Core.DTO.Responses;
using Accounts.Core.Entities;
using Accounts.Core.Handlers;
using Accounts.Core.Providers;
using Accounts.Core.Repositories;

namespace Accounts.Application.Handlers
{
    public class UserAuthorizationHandler : IUserAuthorizationHandler
    {
        private const string MSG_USER_OR_PASSAWORD_INVALID = "User or password invalid";
        private const string MSG_PROFILE_NOT_FOUND_FOR_USER = "Profile not found for user";

        private readonly IUserRepository _userRepository;
        private readonly IUserHandler _userHandler;
        private readonly IUserProfileHandler _userProfileHandler;
        private readonly IPasswordProvider _passwordProvider;
        private readonly ITokenHandler _tokenHandler;

        public UserAuthorizationHandler(
            IUserRepository userRepository,
            IUserHandler userHandler,
            IUserProfileHandler userProfileHandler,
            IPasswordProvider passwordProvider,
            ITokenHandler tokenHandler)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _userHandler = userHandler ?? throw new ArgumentNullException(nameof(userHandler));
            _userProfileHandler = userProfileHandler ?? throw new ArgumentNullException(nameof(userProfileHandler));
            _passwordProvider = passwordProvider ?? throw new ArgumentNullException(nameof(passwordProvider));
            _tokenHandler = tokenHandler ?? throw new ArgumentNullException(nameof(tokenHandler));
        }

        public async Task<AppTokenResponse> RegisterAsync(RegisterRequest request)
        {
            var user = await _userHandler.GetOrCreateByEmailAsync(request);

            _ = await _userProfileHandler.GetOrCreateAsync(new UserProfileRequest{
                UserId = user.Id,
                PrifileId = request.ProfileId,
                AppId = request.AppId
            });

            return await AuthenticationAsync(new LoginRequest{
                Email = request.Email,
                Password = request.Password,
                AppId = request.AppId
            });
        }

        public async Task<AppTokenResponse> AuthenticationAsync(LoginRequest request)
        {
            var userDb = await _userRepository.GetByEmail(request.Email);

            if(userDb == null)
                throw new AuthenticationException(MSG_USER_OR_PASSAWORD_INVALID);

            var passwordHash = _passwordProvider.HashPassword(request.Password, userDb.Salt);

            if(userDb == null || userDb.PasswordHash != passwordHash)
                throw new AuthenticationException(MSG_USER_OR_PASSAWORD_INVALID);

            await ValidateUserProfileAsync(userDb, new RefrashUserRequest{ UserId = userDb.Id, AppId = request.AppId });

            return _tokenHandler.Create(userDb, request);
        }

        public async Task<AppTokenResponse> RefrashAsync(RefrashUserRequest request)
        {
            var userDb = await _userRepository.GetByIdAsync(request.UserId.Value);

            if(userDb == null)
                throw new AuthenticationException(MSG_USER_OR_PASSAWORD_INVALID);

            await ValidateUserProfileAsync(userDb, request);
                
            return _tokenHandler.Create(userDb, new LoginRequest{
                Email = userDb.Email,
                AppId = request.AppId
            });
        }

        private async Task ValidateUserProfileAsync(User user, RefrashUserRequest request)
        {
            if(request.AppId == null)
                return;

            var userProfileExist = await _userProfileHandler.GetOrCreateAsync(new UserProfileRequest{
                    UserId = user.Id,
                    AppId = request.AppId
                });

            if(userProfileExist == null)
                throw new AuthenticationException(MSG_PROFILE_NOT_FOUND_FOR_USER);
        }
    }
}