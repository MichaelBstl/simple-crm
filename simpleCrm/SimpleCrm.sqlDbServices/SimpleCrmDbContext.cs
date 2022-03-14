using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleCrm.SqlDbServices
{
    public class SimpleCrmDbContext : DbContext
    {
        public SimpleCrmDbContext(DbContextOptions<SimpleCrmDbContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }
    }
}
