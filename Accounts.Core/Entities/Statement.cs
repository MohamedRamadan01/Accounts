﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounts.Core.Entities
{
    public class Statement
    {
        public int ID { get; set; }

        [Required]
        public int AccountID { get; set; }

        [Required]
        [StringLength(10)]
        public string DateField { get; set; }

        [Required]
        [StringLength(20)]
        public string Amount { get; set; }

        [ForeignKey("AccountID")]
        public Account Account { get; set; }
    }
}
