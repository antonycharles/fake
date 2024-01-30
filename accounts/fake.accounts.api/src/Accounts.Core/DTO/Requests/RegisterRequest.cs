namespace Accounts.Core.DTO.Requests
{
    public class RegisterRequest : UserRequest
    {
        public int? ProfileId { get; set;}
    }
}