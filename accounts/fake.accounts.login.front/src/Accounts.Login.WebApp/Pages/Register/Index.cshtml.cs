using System.Security.Claims;
using Accounts.Login.Core.Handlers.Interfaces;
using Accounts.Login.Core.Models.Register;
using Accounts.Login.WebApp.PageBase;
using Microsoft.AspNetCore.Mvc;

namespace Accounts.Login.WebApp.Pages.Register;

public class IndexModel : PageModelBase
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

    public async Task<IActionResult> OnGetAsync(string? appId)
    {
        
        Register = new RegisterRequest();
        var appIdGuid = ConvertAppId(appId);

        if(IsAuthenticated)
        {
            var userToken = await _loginHandler.RefrashAsync(appId:appIdGuid,userId:UserId);
            return CallbackLogin(userToken);
        }
        Register.AppId = appIdGuid;

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        try{
            if (!ModelState.IsValid)
                return Page();

            var userToken = await _loginHandler.RegisterAsync(Register);
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