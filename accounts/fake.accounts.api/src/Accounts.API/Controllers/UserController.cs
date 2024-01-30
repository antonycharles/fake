using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Accounts.Core.DTO.Requests;
using Accounts.Core.DTO.Responses.User;
using Accounts.Core.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Accounts.API.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserHandler _userHandler;

        public UserController(ILogger<UserController> logger, IUserHandler userHandler)
        {
            _logger = logger;
            _userHandler = userHandler;
        }

        [HttpGet]
        [ProducesResponseType(typeof(UserPaginationResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAsync([FromQuery] PaginationRequest request)
        {
            try
            {
                var result = await _userHandler.GetPaginationAsync(request);
                return Ok(result);
            }
            catch(Exception e)
            {
                _logger.LogError(e, e.Message);
                return Problem(e.Message, statusCode: StatusCodes.Status500InternalServerError);
            }
        }
    }
}