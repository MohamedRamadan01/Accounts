using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounts.Core.Dtos
{
    public class StatementDto
    {
        public int ID { get; set; }

        [StringLength(10)]
        public string DateField { get; set; }

        [Required]
        [StringLength(20)]
        public string Amount { get; set; }
        public AccountDto Account { get; set; }
    }
}
