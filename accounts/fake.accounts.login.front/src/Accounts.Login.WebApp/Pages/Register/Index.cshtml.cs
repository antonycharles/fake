using System.Security.Claims;
using Accounts.Login.Core.Handlers.Interfaces;
using Accounts.Login.Core.Models.Register;
using Accounts.Login.Core.Models.Token;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Accounts.Login.WebApp.Pages.Register;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly ILoginHandler _loginHandler;
    
    [BindProperty]
    public RegisterRequest Register { get; set; }

    public IndexModel(ILogger<IndexModel> logger, ILoginHandler loginHandler)
    {
        _logger = logger;
        _loginHandler = loginHandler;
    }

    public IActionResult OnGet(string? appId)
    {
        if(User.Identity.IsAuthenticated){
            var id = User.Claims.First(s => s.Type == ClaimTypes.Sid).Value;
            return RedirectToPage("../index");
        }

        Register = new RegisterRequest();
        if(Guid.TryParse(appId, out _)){
            Register.AppId = Guid.Parse(appId);
        }

        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        try{
            if (!ModelState.IsValid)
            {
                if(Register.AppId == Guid.Empty)
                    ModelState.AddModelError("Form.error", "AppId not found!");

                return Page();
            }

            var userToken = await _loginHandler.RegisterAsync(Register);
            await CreateCookieUserAsync(userToken);

            return RedirectToPage("../index");
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("Form.error", ex.Message);
            return Page();
        }
        
    }

    public async Task CreateCookieUserAsync(AppTokenResponse userToken)
    {
        var userClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Sid, userToken.User.Id.ToString()),
                new Claim(ClaimTypes.Name, userToken.User.Name),
                new Claim(ClaimTypes.Email, userToken.User.Email),
            };
            var minhaIdentity = new ClaimsIdentity(userClaims, "Usuario");
            var userPrincipal = new ClaimsPrincipal(new[] { minhaIdentity });
                
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme, 
                userPrincipal, 
                new AuthenticationProperties{
                    IsPersistent = true
                }
            );
    }
}