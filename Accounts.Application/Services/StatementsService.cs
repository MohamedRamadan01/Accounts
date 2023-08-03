using System;
using System.Collections.Generic;
using System.Linq;
using Accounts.Core.Interfaces.Services;
using Accounts.Core.Entities;
using Accounts.Core.Interfaces.Infrastructure;
using Accounts.Core.Dtos;

public class StatementsService : IStatementService
{
    private readonly IUnitOfWork _unitOfWork;

    public StatementsService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public IEnumerable<Statement> GetStatements(SearchParameters searchParameters)
    {
        var statements = _unitOfWork.Repository<Statement>().GetAll().ToList();

        if (searchParameters == null ||
            (searchParameters.AccountId == null && searchParameters.FromDate == null && searchParameters.ToDate == null 
            && searchParameters.FromAmount == null && searchParameters.ToAmount == null))
        {
            // If no search parameters are provided, filter statements for the last three months
            var threeMonthsAgo = DateTime.Now.AddMonths(-3);
            statements = statements.Where(s => DateTime.Parse(s.DateField) >= threeMonthsAgo).ToList();
        }
        else
        {
            // Apply other search filters if search parameters are provided
            if (!string.IsNullOrEmpty(searchParameters.AccountId))
            {
                statements = statements.Where(s => s.Account_id == searchParameters.AccountId).ToList();
            }

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
        }

        return statements.ToList();
    }

}
