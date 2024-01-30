using Accounts.Core.DTO.Responses;

namespace Accounts.Application.Mappers.UserMappers
{
    public static class UserResponseMapper
    {
        public static UserResponse ToResponse(this Core.Entities.User entity) => new UserResponse
        {
            Id = entity.Id,
            Name = entity.Name,
            Email = entity.Email,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt,
            Status = entity.Status
        };
    }
}