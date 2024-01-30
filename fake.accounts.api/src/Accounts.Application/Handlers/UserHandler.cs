using Accounts.Application.Exceptions;
using Accounts.Application.Mappers.UserMappers;
using Accounts.Core.Builders;
using Accounts.Core.DTO.Requests;
using Accounts.Core.DTO.Responses;
using Accounts.Core.DTO.Responses.User;
using Accounts.Core.Enums;
using Accounts.Core.Handlers;
using Accounts.Core.Providers;
using Accounts.Core.Repositories;

namespace Accounts.Application.Handlers
{
    public class UserHandler : IUserHandler
    {
        private const string MSG_USER_ALREADY_EXISTS = "User already exists";
        private readonly IUserRepository _userRepository;
        private readonly IPasswordProvider _passwordProvider;
        public readonly IUserPaginationResponseBuilder _userPaginationResponseBuilder;

        public UserHandler(
            IUserRepository userRepository, 
            IPasswordProvider passwordProvider,
            IUserPaginationResponseBuilder userPaginationResponseBuilder)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _passwordProvider = passwordProvider ?? throw new ArgumentNullException(nameof(passwordProvider));
            _userPaginationResponseBuilder = userPaginationResponseBuilder ?? throw new ArgumentNullException(nameof(userPaginationResponseBuilder));
        }

        public async Task<UserResponse> CreateAsync(UserRequest userRequest)
        {
            var userExist = await _userRepository.AnyAsync(w => w.Email == userRequest.Email);
             
            if(userExist)
                throw new ConflictException(MSG_USER_ALREADY_EXISTS);

            var salt = _passwordProvider.GenerateSalt();
            string passwordHash = _passwordProvider.HashPassword(userRequest.Password, salt);

            var user = userRequest.ToUser();
            user.PasswordHash = passwordHash;
            user.Salt = salt;
            user.Status = StatusEnum.Active;
            user.CreatedAt = DateTime.UtcNow;

            user = await _userRepository.AddAsync(user);

            return user.ToResponse();
        }

        public async Task<UserResponse> GetOrCreateByEmailAsync(UserRequest request)
        {
            var user = await _userRepository.GetByEmail(request.Email);
            
            if(user != null)
                return user.ToResponse();

            return await CreateAsync(request);
        }

        public async Task<UserPaginationResponse> GetPaginationAsync(PaginationRequest paginationRequest)
        {
            var totalRecords = await _userRepository.CountPaginationAsync(paginationRequest.Search);
            var users = await _userRepository.GetPaginationAsync(paginationRequest.Search, paginationRequest.CurrentPage, paginationRequest.Take);
            return _userPaginationResponseBuilder.Build(paginationRequest, users, totalRecords);   
        }
    }
}