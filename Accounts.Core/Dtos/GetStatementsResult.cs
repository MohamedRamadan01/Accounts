using Accounts.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounts.Core.Dtos
{
    public class GetStatementsResult
    {
        public bool IsSuccess = true;
        public IEnumerable<StatementDto> statements { get; set; }
        public string ErrorMessage = string.Empty;
    }
}
