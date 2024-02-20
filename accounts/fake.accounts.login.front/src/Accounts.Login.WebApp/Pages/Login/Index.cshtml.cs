using Accounts.Login.Core.Handlers.Interfaces;
using Accounts.Login.Core.Models.Login;
using Accounts.Login.WebApp.PageBase;
using Microsoft.AspNetCore.Mvc;

namespace Accounts.Login.WebApp.Pages.Login;

public class IndexModel : PageModelBase
{
    private readonly ILogger<IndexModel> _logger;
    private readonly ILoginHandler _loginHandler;
    [BindProperty]
    public LoginRequest Login { get; set; }

    public IndexModel(ILogger<IndexModel> logger, ILoginHandler loginHandler)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _loginHandler = loginHandler ?? throw new ArgumentNullException(nameof(loginHandler));
    }

    public async Task<IActionResult> OnGetAsync(string appId)
    {
        try
        {
            Login = new LoginRequest();
            var appIdGuid = ConvertAppId(appId);
            if(IsAuthenticated)
            {
                var userToken = await _loginHandler.RefrashAsync(appId:appIdGuid,userId:UserId);
                return CallbackLogin(userToken);
            }

            Login.AppId = appIdGuid;
            return Page();
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("Form.error", ex.Message);
            return Page();
        }
    }

    public async Task<IActionResult> OnPostAsync()
    {
        try
        {
            if (!ModelState.IsValid)
                return Page();

            var userToken = await _loginHandler.AuthenticationAsync(Login);
            await CreateCookieUserAsync(userToken);

            return CallbackLogin(userToken);
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("Form.error", ex.Message);
            return Page();
        }
    }
}
