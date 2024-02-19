using System.Security.Authentication;
using System.Security.Claims;
using Accounts.Login.Core.Models.Token;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Accounts.Login.WebApp.PageBase
{
    public abstract class PageModelBase : PageModel
    {
        public async Task CreateCookieUserAsync(AppTokenResponse userToken)
        {
            List<Claim> userClaims = CreateUserClaims(userToken);

            var identity = new ClaimsIdentity(userClaims,"User");
            var userPrincipal = new ClaimsPrincipal(new[] { identity });

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                userPrincipal,
                new AuthenticationProperties
                {
                    IsPersistent = true
                }
            );
        }

        private static List<Claim> CreateUserClaims(AppTokenResponse userToken)
        {
            return new List<Claim>()
            {
                new Claim(ClaimTypes.Sid, userToken.User.Id.ToString()),
                new Claim(ClaimTypes.Name, userToken.User.Name),
                new Claim(ClaimTypes.Email, userToken.User.Email),
            };
        }

        protected IActionResult CallbackLogin(AppTokenResponse userToken)
        {
            if(userToken.CallbackUrl is not null)
                return Redirect(userToken.CallbackUrl);

            return RedirectToPage("../index");
        }

        protected bool IsAuthenticated => User?.Identity?.IsAuthenticated ?? false;

        protected Guid UserId {
            get{
                if(!IsAuthenticated)
                    throw new AuthenticationException("User not authentication");
                
                return  Guid.Parse(User.Claims.First(s => s.Type == ClaimTypes.Sid).Value);
            }
        }

        protected string UserName {
            get{
                if(!IsAuthenticated)
                    throw new AuthenticationException("User not authentication");
                
                return User.Claims.First(s => s.Type == ClaimTypes.Name).Value;
            }
        }

        protected string UserEmail {
            get{
                if(!IsAuthenticated)
                    throw new AuthenticationException("User not authentication");
                
                return User.Claims.First(s => s.Type == ClaimTypes.Email).Value;
            }
        }

        protected Guid ConvertAppId(string appId)
        {
            if(!Guid.TryParse(appId, out _))
                throw new ArgumentException("AppId not informed");
            
            return Guid.Parse(appId);
        }
    }
}