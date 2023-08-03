using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Accounts.Core.Entities;  
using Accounts.Core.Dtos;
using Accounts.Core.Interfaces.Services;

namespace YourNamespace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
                if (searchParameters != null)
                {
                    // Perform additional request validation
                    if (!string.IsNullOrEmpty(searchParameters.AccountId) && !IsValidAccountId(searchParameters.AccountId))
                    {
                        return BadRequest("Invalid account ID.");
                    }
                }

                var statements = _statementService.GetStatements(searchParameters);
                return Ok(statements);
            }
            catch (FormatException)
            {
                return BadRequest("Invalid format for date or amount.");
            }
            catch (ServiceException ex)
            {
                // Map service exception to appropriate HTTP response
                return StatusCode((int)ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {
                // Log the exception for debugging
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        private bool IsValidAccountId(string accountId)
        {
            // Implement your validation logic here
            // Example: Check if the account ID format is valid
            // You can return true or false based on your validation criteria
            return true;
        }

    }
}
