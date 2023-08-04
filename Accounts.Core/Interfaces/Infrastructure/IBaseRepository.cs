using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Accounts.Core.Interfaces.Infrastructure
{
    public interface IBaseRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        IQueryable<T> Include(params Expression<Func<T, object>>[] includes);
    }
}
