using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleCrm.SqlDbServices
{
    public class SqlCustomerData : ICustomerData
    {
        private readonly SimpleCrmDbContext dbContext;

        public SqlCustomerData(SimpleCrmDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public Customer Get(int id)
        {
            return dbContext.Customers.FirstOrDefault((item) => item.Id == id);
        }

        public IEnumerable<Customer> GetAll()
        {
            return dbContext.Customers;
        }

        public void Add(Customer customer)
        {
            dbContext.Add(customer);
            dbContext.SaveChanges();
        }
        public void Update()
        {
            dbContext.SaveChanges();
        }
    }
}
