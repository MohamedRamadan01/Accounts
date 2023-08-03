using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounts.Core.Entities
{
    public class Statement
    {
        public int ID { get; set; }

        [Required]
        public string Account_id { get; set; }

        [Required]
        [StringLength(10)]
        public string DateField { get; set; }

        [Required]
        [StringLength(20)]
        public string Amount { get; set; }
    }
}
