using System;
using System.Collections.Generic;
using System.Linq;
using Accounts.Core.Interfaces.Services;
using Accounts.Core.Entities;
using Accounts.Core.Interfaces.Infrastructure;
using Accounts.Core.Dtos;
using System.Text;
using System.Security.Cryptography;

public class StatementsService : IStatementService
{
    private readonly IUnitOfWork _unitOfWork;

    public StatementsService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public GetStatementsResult GetStatements()
    {
        GetStatementsResult getStatementsResult = new();
        var statements = _unitOfWork.Repository<Statement>().Include(s => s.Account).ToList();
        getStatementsResult.statements = statements.Select(statement => new StatementDto
        {
            ID = statement.ID,
            DateField = statement.DateField,
            Amount = statement.Amount,
            Account = new AccountDto
            {
                ID = ComputeHash(statement.AccountID),
                AccountType = statement.Account.AccountType,
                AccountNumber = statement.Account.AccountNumber,
            }
        }).ToList();

        return getStatementsResult;
    }
    public GetStatementsResult GetStatements(SearchParameters searchParameters)
    {
        GetStatementsResult getStatementsResult = new();
        var statements = _unitOfWork.Repository<Statement>().Include(s=>s.Account).ToList();

        if (searchParameters == null ||
            (searchParameters.AccountId == 0 && searchParameters.FromDate == null && searchParameters.ToDate == null
            && searchParameters.FromAmount == null && searchParameters.ToAmount == null))
        {
            // If no search parameters are provided, filter statements for the last three months
            statements = GetLastThreeMonthsStatements(statements);
        }
        else
        {
            if (searchParameters.AccountId > 0)
            {
                statements = GetStatmentByAccountId(statements, searchParameters.AccountId);

                if (statements.Count == 0)
                {
                    getStatementsResult.IsSuccess = false;
                    getStatementsResult.ErrorMessage = "Invalid account ID.";
                    return getStatementsResult;
                }

            }
            statements = GetFilteredStatements(searchParameters, statements);
        }

        getStatementsResult.statements = statements.Select(statement => new StatementDto
        {
            ID = statement.ID,
            DateField = statement.DateField,
            Amount = statement.Amount,
            Account = new AccountDto { 
                        ID = ComputeHash(statement.AccountID),
                        AccountType = statement.Account.AccountType,
                        AccountNumber = statement.Account.AccountNumber,
            }
        }).ToList();

        return getStatementsResult;
    }

    public List<Statement> GetLastThreeMonthsStatements(List<Statement> statements)
    {
        var threeMonthsAgo = DateTime.Now.AddMonths(-3);
        statements = statements.Where(s => DateTime.Parse(s.DateField) >= threeMonthsAgo).ToList();
        return statements;
    }

    public List<Statement> GetStatmentByAccountId(List<Statement> statements, int accountId)
    {
        statements = statements.Where(s => s.AccountID == accountId).ToList();
        return statements;
    }

    public List<Statement> GetFilteredStatements(SearchParameters searchParameters, List<Statement> statements)
    {

        // Apply other search filters if search parameters are provided
        if (searchParameters.FromDate.HasValue && searchParameters.ToDate.HasValue)
        {
            statements = statements.Where(s =>
                DateTime.Parse(s.DateField) >= searchParameters.FromDate.Value &&
                DateTime.Parse(s.DateField) <= searchParameters.ToDate.Value).ToList();
        }

        if (searchParameters.FromAmount.HasValue && searchParameters.ToAmount.HasValue)
        {
            statements = statements.Where(s =>
                decimal.Parse(s.Amount) >= searchParameters.FromAmount.Value &&
                decimal.Parse(s.Amount) <= searchParameters.ToAmount.Value).ToList();
        }

        if (searchParameters.FromAmount.HasValue && !searchParameters.ToAmount.HasValue)
        {
            statements = statements.Where(s =>
                decimal.Parse(s.Amount) >= searchParameters.FromAmount.Value).ToList();
        }

        if (!searchParameters.FromAmount.HasValue && searchParameters.ToAmount.HasValue)
        {
            statements = statements.Where(s =>
                decimal.Parse(s.Amount) <= searchParameters.ToAmount.Value).ToList();
        }

        return statements.ToList();
    }

    public static string ComputeHash(int id)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] bytes = Encoding.UTF8.GetBytes(id.ToString());
            byte[] hashBytes = sha256.ComputeHash(bytes);
            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }
    }
}
