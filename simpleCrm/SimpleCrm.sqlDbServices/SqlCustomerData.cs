using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Dynamic.Core;
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
        }
        public void Update(Customer customer)
        {
            //Update is not needed, since changes are tracked by EF
        }
        public void Commit()
        {
            dbContext.SaveChanges();
        }

        public List<Customer> GetAll(int pageIndex, int take, string orderBy)
        {
            //valid columns to sort by
            var validColumns = new string[] { "Id", "FirstName", "LastName", "PhoneNumber", "OptInNewsletter", "Type", "EmailAddress", "ContactMethod", "Status", "LastContactDate" }.ToList();
            // default to last name sort order if orderBy is blank or null
            if (string.IsNullOrWhiteSpace(orderBy))
            {
                orderBy = "LastName";
            }
            var splitOrderBy = orderBy.Split(',');
            // examine each sort parameter & make sure it has the proper number of parameters & the parameters are 
            //   valid columns & sort direction
            foreach (string order in splitOrderBy)
            {
                var component = order.Split(" ");
                if (component.Length == 1 || component.Length == 2)
                { //valid as it has one or two arguments
                }
                else
                {
                    throw new Exception("Invalid sort parameter. Incorrect number of arguments:" + order);
                }
                if (!validColumns.Contains(component[0].ToLower()))
                {
                    throw new Exception("Invalid sort parameter. Incorrect column specified:" + component);
                }


                if (!(component[1].ToLower().Contains("asc") ||
                     component[1].ToLower().Contains("desc") ||
                     string.IsNullOrWhiteSpace(component[1])
                     ))
                {
                    throw new Exception("Invalid sort parameter. Incorrect sort order:" + component);
                }
            }


            return dbContext.Customers
                .OrderBy(orderBy)
                .Skip(pageIndex * take)
                .Take(take).ToList();

        }
        public void Delete(Customer customer)
        {
            dbContext.Remove(customer);
        }
        public void Delete(int id)
        {
            var cust = new Customer { Id = id };
            dbContext.Attach(cust);
            dbContext.Remove(cust);
        }
    }
}
