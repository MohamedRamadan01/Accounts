using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounts.Core.Dtos
{
    public class SearchParameters
    {
        public int AccountId { get; set; } = 0;
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public decimal? FromAmount { get; set; }
        public decimal? ToAmount { get; set; }
    }
}
