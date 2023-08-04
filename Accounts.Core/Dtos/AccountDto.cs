using Accounts.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounts.Core.Dtos
{
    public class AccountDto
    {
        public string ID { get; set; }
        public string AccountType { get; set; }
        public string AccountNumber { get; set; }
    }
}
