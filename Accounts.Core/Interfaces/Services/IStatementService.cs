using Accounts.Core.Dtos;
using Accounts.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounts.Core.Interfaces.Services
{
    public interface IStatementService
    {
        IEnumerable<Statement> GetStatements(SearchParameters searchParameters);
    }
}
