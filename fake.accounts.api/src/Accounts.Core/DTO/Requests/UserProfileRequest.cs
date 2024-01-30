namespace Accounts.Core.DTO.Requests
{
    public class UserProfileRequest
    {
        public Guid UserId { get; set; }
        public int? PrifileId { get; set; }
        public Guid? AppId { get; set; }
    }
}