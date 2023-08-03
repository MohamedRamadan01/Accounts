using Accounts.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Accounts.Infrastracture.Data
{
    public class AccountsDbContext : DbContext
    {

        public AccountsDbContext(DbContextOptions<AccountsDbContext> options) : base(options)
        {

        }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Statement> Statements { get; set; }

        
    }
}
