using Accounts.Core.DTO.Responses;

namespace Accounts.Application.Mappers.UserProfileMappers
{
    public static class UserProfileResponseMapper
    {
        public static UserProfileResponse ToResponse(this Core.Entities.UserProfile entity) => new UserProfileResponse
        {
            PrifileId = entity.ProfileId,
            UserId = entity.UserId
        };
        
    }
}