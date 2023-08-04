using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounts.Core.Entities
{
    public class Account
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string AccountType { get; set; }

        [Required]
        [StringLength(20)]
        public string AccountNumber { get; set; }

        public List<Statement> Statements { get; set; } = new();

    }

}
