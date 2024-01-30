using System.Security.Claims;
using Accounts.Core.DTO.Requests;
using Accounts.Core.DTO.Responses;
using Accounts.Core.Entities;
using Accounts.Core.Enums;
using Accounts.Core.Handlers;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace Accounts.Application.Handlers
{
    public class TokenHandler : ITokenHandler
    {
        private readonly ITokenKeyHandler _tokenKeyHandler;
        private const string T_ISSUER = "www.fakeaccounts.com";
        private const string T_AUDIENCE = "fake.accounts.api";

        public TokenHandler(ITokenKeyHandler tokenKeyHandler)
        {
            _tokenKeyHandler = tokenKeyHandler ?? throw new ArgumentNullException(nameof(tokenKeyHandler));
        }

        public TokenResponse Create(Client client)
        {
            var profiles = client.ClientsProfiles?
                    .Where(w => w.Profile.Status == StatusEnum.Active)
                    .Select(s => s.Profile)
                    .ToList();

            var roles = GetRoles(profiles);

            var claims = new List<Claim>
            {
                new Claim("id", client.Id.ToString()),
                new Claim("name", client.Name),
                new Claim("type","client")
            };

            foreach (var role in roles)
                claims.Add(new Claim("roles", role));

            return CreateTokenResponse(claims);
        }


        public AppTokenResponse Create(User user, LoginRequest request)
        {
            List<Profile> profiles = user.UsersProfiles?
                .Where(w => w.Profile.Status == StatusEnum.Active && w.Profile.AppId == request.AppId)
                .Select(s => s.Profile)
                .ToList();

            var roles = GetRoles(profiles);

            var claims = new List<Claim>
            {
                new Claim("id", user.Id.ToString()),
                new Claim("name", user.Name),
                new Claim("type","user")
            };

            foreach (var role in roles)
                claims.Add(new Claim("roles", role));

            return new AppTokenResponse{
                AppId = profiles.FirstOrDefault()?.AppId,
                User = CreateUserResponse(user),
                CallbackUrl = profiles.FirstOrDefault()?.App?.CallbackUrl,
                Token = CreateTokenResponse(claims)
            };
        }

        private TokenResponse CreateTokenResponse(List<Claim> claims)
        {
            SecurityTokenDescriptor jwt = GetSecurityTokenDescriptor(claims);

            var tokenHandler = new JsonWebTokenHandler();

            var key = _tokenKeyHandler.GetKey();

            jwt.SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.EcdsaSha256);
            var lastJws = tokenHandler.CreateToken(jwt);

            return new TokenResponse
            {
                ExpiresIn = jwt.Expires,
                Token = lastJws
            };
        }

        private SecurityTokenDescriptor GetSecurityTokenDescriptor(List<Claim> claims)
        {
            return new SecurityTokenDescriptor
            {
                Issuer = T_ISSUER,
                Audience = T_AUDIENCE,
                IssuedAt = DateTime.Now,
                NotBefore = DateTime.Now,
                Expires = DateTime.Now.AddHours(1),
                Subject = new ClaimsIdentity(claims)
            };
        }


        private List<string> GetRoles(List<Profile> profiles)
        {
            List<string> roles = new List<string>();

            if(profiles == null)
                return roles;

            foreach (var profile in profiles)
            {
                var rolesProfile = profile.ProfilesRoles
                    .Where(w => w.Role.Status == StatusEnum.Active)
                    .Select(s => $"{profile.App.Slug}.{s.Role.Slug}")
                    .ToList();

                roles.AddRange(rolesProfile);
            }

            return roles;
        }

        private UserResponse CreateUserResponse(User user)
        {
            return new UserResponse{
                Id = user.Id,
                Name = user.Name,
                Email = user.Email
            };
        }
    }
}