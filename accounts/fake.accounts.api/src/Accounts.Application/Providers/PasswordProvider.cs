using Accounts.Core.Providers;

namespace Accounts.Application.Providers
{
    public class PasswordProvider : IPasswordProvider
    {
        public string GenerateSalt()
        {
            return BCrypt.Net.BCrypt.GenerateSalt();
        }

        public string HashPassword(string password, string salt)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, salt);
        }
    }
}