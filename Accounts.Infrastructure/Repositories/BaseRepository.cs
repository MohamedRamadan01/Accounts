using Microsoft.EntityFrameworkCore;
using Accounts.Core.Entities;
using Accounts.Infrastracture.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Accounts.Core.Interfaces.Infrastructure;

namespace Accounts.Infrastracture.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _dbSet;

        public BaseRepository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet;
        }
     
        public IQueryable<T> Include(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>();
            if (includes != null)
                foreach (Expression<Func<T, object>> include in includes)
                    query = query.Include(include);

            return query;
        }

    }
}
