using Accounts.Core.Enums;

namespace Accounts.Core.DTO.Responses
{
    public class UserResponse : BaseResponse
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public StatusEnum Status { get; set; }
    }
}