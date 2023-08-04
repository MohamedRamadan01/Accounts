using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Accounts.Core.Entities;
using Accounts.Core.Dtos;
using Accounts.Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Accounts.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StatementsController : ControllerBase
    {
        private readonly IStatementService _statementService;

        public StatementsController(IStatementService statementService)
        {
            _statementService = statementService;
        }

        [HttpGet]
        public IActionResult GetStatements([FromQuery] SearchParameters searchParameters)
        {
            try
            {
                var userClaims = User.Claims;
                var userRoleClaim = userClaims.FirstOrDefault(c => c.Type == "Role")?.Value;

                if (userRoleClaim != "Admin")
                {
                    return Unauthorized();
                }
                var statementsResult = _statementService.GetStatements(searchParameters);
                if (statementsResult.IsSuccess)
                {
                    return Ok(statementsResult.statements);
                }
                else
                {
                    return BadRequest(statementsResult.ErrorMessage);
                }
            }
            catch (FormatException)
            {
                return BadRequest("Invalid format for date or amount.");
            }
            catch (Exception ex)
            {
                // Log the exception for debugging
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpGet]
        public IActionResult GetStatements()
        {
            try
            {
                var userClaims = User.Claims;
                var userRoleClaim = userClaims.FirstOrDefault(c => c.Type == "Role")?.Value;

                if (userRoleClaim != "User")
                {
                    return Unauthorized();
                }
                var statementsResult = _statementService.GetStatements();
                if (statementsResult.IsSuccess)
                {
                    return Ok(statementsResult.statements);
                }
                else
                {
                    return BadRequest(statementsResult.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                // Log the exception for debugging
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

    }
}
