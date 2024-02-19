using Accounts.Application.Exceptions;
using Accounts.Core.DTO.Requests;
using Accounts.Core.DTO.Responses;
using Accounts.Core.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace Accounts.API.Controllers;

[ApiController]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/client/authorization")]
public class ClientAuthenticationController : ControllerBase
{
    private readonly ILogger<ClientAuthenticationController> _logger;
    private readonly IClientAuthorizationHandler _clientAuthorizationHandler;

    public ClientAuthenticationController(
        ILogger<ClientAuthenticationController> logger,
        IClientAuthorizationHandler clientAuthorizationHandler)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _clientAuthorizationHandler = clientAuthorizationHandler ?? throw new ArgumentNullException(nameof(clientAuthorizationHandler));
    }

    [HttpPost]
    [ProducesResponseType(typeof(TokenResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AuthenticationAsync(ClientLoginRequest request)
    {
        try
        {
            var result = await _clientAuthorizationHandler.AuthenticationAsync(request);
            return Ok(result);
        }
        catch(AuthenticationException e)
        {
            _logger.LogError(e, e.Message);
            return Problem(e.Message, statusCode: StatusCodes.Status400BadRequest);
        }
        catch(Exception e)
        {
            _logger.LogError(e, e.Message);
            return Problem(e.Message, statusCode: StatusCodes.Status500InternalServerError);
        }
    }
}
