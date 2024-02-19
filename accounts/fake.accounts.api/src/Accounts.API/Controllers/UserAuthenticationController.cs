using Accounts.Application.Exceptions;
using Accounts.Core.DTO.Requests;
using Accounts.Core.DTO.Responses;
using Accounts.Core.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Accounts.API.Controllers;

[Authorize]
[ApiController]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/user/authentication")]
public class UserAuthenticationController : ControllerBase
{
    private readonly ILogger<UserAuthenticationController> _logger;
    private readonly IUserAuthorizationHandler _userAuthorizationHandler;
    public UserAuthenticationController(
        IUserAuthorizationHandler authorizationHandler, 
        ILogger<UserAuthenticationController> logger)
    {
        _userAuthorizationHandler = authorizationHandler ?? throw new ArgumentNullException(nameof(authorizationHandler));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [HttpPost("login")]
    [Authorize(Roles = "acapi.user.authentication")]
    [ProducesResponseType(typeof(AppTokenResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> LoginAsync(LoginRequest request)
    {
        try
        {
            var result = await _userAuthorizationHandler.AuthenticationAsync(request);
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


    [HttpPost("signup")]
    [Authorize(Roles = "acapi.user.authentication")]
    [ProducesResponseType(typeof(AppTokenResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> SignupAsync(RegisterRequest request)
    {
        try
        {
            var result = await _userAuthorizationHandler.RegisterAsync(request);
            return Created("",result);
        }
        catch(DataValidationException e)
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

    [HttpPost("refrash")]
    [Authorize(Roles = "acapi.user.authentication")]
    [ProducesResponseType(typeof(AppTokenResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RefrashAsync(RefrashUserRequest request)
    {
        try
        {
            var result = await _userAuthorizationHandler.RefrashAsync(request);
            return Ok(result);
        }
        catch(DataValidationException e)
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
