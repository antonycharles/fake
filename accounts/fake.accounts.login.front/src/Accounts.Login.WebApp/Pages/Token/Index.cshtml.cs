using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;
using Accounts.Login.Core.Models.User;
using Accounts.Login.WebApp.PageBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Accounts.Login.WebApp.Pages.Token
{
    public class Index : PageModelBase
    {
        private readonly ILogger<Index> _logger;

        public Index(ILogger<Index> logger)
        {
            _logger = logger;
        }

        public JsonResult OnGet()
        {
            try
            {
                if(!IsAuthenticated)
                    throw new AuthenticationException("User not authorized");

                Response.StatusCode = StatusCodes.Status200OK;
                return new JsonResult(new UserResponse { Id = Guid.NewGuid(), Name = UserName });

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