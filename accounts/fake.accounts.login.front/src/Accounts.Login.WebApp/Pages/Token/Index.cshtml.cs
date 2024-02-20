using System.Security.Authentication;
using Accounts.Login.Core.Handlers;
using Accounts.Login.WebApp.PageBase;
using Microsoft.AspNetCore.Mvc;

namespace Accounts.Login.WebApp.Pages.Token
{
    public class Index : PageModelBase
    {
        private readonly ILogger<Index> _logger;
    private readonly ILoginTokenHandler _loginTokenHandler;

        public Index(ILogger<Index> logger, ILoginTokenHandler loginTokenHandler)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _loginTokenHandler = loginTokenHandler ?? throw new ArgumentNullException(nameof(loginTokenHandler));
        }

        public async Task<JsonResult> OnGetAsync(string accessToken)
        {
            try
            {
                if(!IsAuthenticated || accessToken is null)
                    throw new AuthenticationException("User not authorized");

                var token = await _loginTokenHandler.GetTokenAsync(accessToken);

                Response.StatusCode = StatusCodes.Status200OK;
                return new JsonResult(token);

            }
            catch(AuthenticationException ex)
            {
                Response.StatusCode = StatusCodes.Status401Unauthorized;
                return new JsonResult(new ProblemDetails{
                    Title = "Not authorized",
                    Detail = ex.Message,
                    Status = StatusCodes.Status401Unauthorized
                });

            }
            catch(Exception ex)
            {
                Response.StatusCode = StatusCodes.Status500InternalServerError;
                return new JsonResult(new ProblemDetails{
                    Title = "Internal Server Error",
                    Detail = ex.Message,
                    Status = StatusCodes.Status500InternalServerError
                });
            }
        }
    }
}