using Accounts.Core.DTO.Requests;

namespace Accounts.Application.Mappers.UserMappers
{
    public static class UserRequestMapper
    {
        public static Core.Entities.User ToUser(this UserRequest request) => new Core.Entities.User
        {
            Name = request.Name,
            Email = request.Email
        };
    }
}