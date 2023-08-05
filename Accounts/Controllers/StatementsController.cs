using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Accounts.Core.Entities;
using Accounts.Core.Dtos;
using Accounts.Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.Extensions.Logging;

namespace Accounts.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StatementsController : ControllerBase
    {
        private readonly IStatementService _statementService;
        private readonly ILogger<StatementsController> _logger;

        public StatementsController(IStatementService statementService, ILogger<StatementsController> logger)
        {
            _statementService = statementService;
            _logger = logger;
        }

        [HttpGet("admin")]
        public IActionResult GetStatements([FromQuery] SearchParameters searchParameters)
        {
            try
            {
                var userClaims = User.Claims;
                var userRoleClaim = userClaims.FirstOrDefault(c => c.Type == "Role")?.Value;


                if (userRoleClaim == null || userRoleClaim != "Admin")
                {
                    _logger.LogInformation($"User Name: {User.Identity.Name}, Role: {userRoleClaim}, Requested: GetStatements has invalid access");
                    return Unauthorized();
                }

                var statementsResult = _statementService.GetStatements(searchParameters);

                if (statementsResult.IsSuccess)
                    return Ok(statementsResult.statements);
                else
                    return BadRequest(statementsResult.ErrorMessage);
            }
            catch (FormatException)
            {
                return BadRequest("Invalid format for date or amount.");
            }
            catch (Exception ex)
            {
                _logger.LogInformation("An error occurred while processing GetStatements with error message : " + ex.Message);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpGet("user")]
        public IActionResult GetStatements()
        {
            try
            {
                var userClaims = User.Claims;
                var userRoleClaim = userClaims.FirstOrDefault(c => c.Type == "Role")?.Value;


                if (userRoleClaim == null || userRoleClaim != "User")
                {
                    _logger.LogInformation($"User Name: {User.Identity.Name}, Role: {userRoleClaim}, Requested: GetStatements has invalid access");
                    return Unauthorized();
                }


                var statementsResult = _statementService.GetStatements();

                if (statementsResult.IsSuccess)
                    return Ok(statementsResult.statements);
                else
                    return BadRequest(statementsResult.ErrorMessage);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("An error occurred while processing GetStatements with error message : " + ex.Message);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

    }
}
