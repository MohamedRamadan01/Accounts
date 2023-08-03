using Accounts.Core.Entities;
using Accounts.Core.Interfaces.Infrastructure;
using Accounts.Infrastracture.Data;
using Accounts.Infrastracture.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Accounts.Infrastracture.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AccountsDbContext _context;
        private Hashtable _repositories = new Hashtable();


        public UnitOfWork(AccountsDbContext context)
        {
            _context = context;
        }

        public IBaseRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            if (_repositories == null) _repositories = new Hashtable();
            var Type = typeof(TEntity).Name;
            if (!_repositories.ContainsKey(Type))
            {
                var repositiryType = typeof(BaseRepository<>);
                var repositoryInstance = Activator.CreateInstance(
                    repositiryType.MakeGenericType(typeof(TEntity)), _context);
                _repositories.Add(Type, repositoryInstance);
            }

            return (IBaseRepository<TEntity>)_repositories[Type];
        }


        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
