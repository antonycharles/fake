using Accounts.Core.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Accounts.API.Controllers;

[Authorize]
[ApiController]
[Route("api/token")]
public class TokenController : ControllerBase
{
    private readonly ILogger<TokenController> _logger;
    private readonly ITokenKeyHandler _tokenKeyHandler;

    public TokenController(
        ITokenKeyHandler tokenKeyHandler, 
        ILogger<TokenController> logger)
    {
        _tokenKeyHandler = tokenKeyHandler ?? throw new ArgumentNullException(nameof(tokenKeyHandler));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [HttpGet]
    [Authorize(Roles = "acapi.token.key")]
    [ProducesResponseType(typeof(IList<JsonWebKey>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public IActionResult Get()
    {
        try
        {
            var result = _tokenKeyHandler.GetPublicKey();
            return Ok(result);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Problem(e.Message, statusCode: StatusCodes.Status500InternalServerError);
        }
    }

}
