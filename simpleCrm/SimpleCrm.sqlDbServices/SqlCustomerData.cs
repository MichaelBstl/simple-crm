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

        public List<Customer> GetAll(CustomerListParameters listParameters)
        {
            //valid columns to sort by
            var validColumns = new string[] { "id", "firstname", "lastname", "phonenumber", "optinnewsletter", "type", "emailaddress", "contactmethod", "status", "lastcontactdate" }.ToList();
            // default to last name sort order if orderBy is blank or null
            if (!string.IsNullOrWhiteSpace(listParameters.OrderBy))
            {
                var splitOrderBy = listParameters.OrderBy.Split(',');
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


                    if (!(component.Length < 2 ||
                          string.IsNullOrWhiteSpace(component[1]) ||
                          component[1].ToLower().Contains("asc") ||
                          component[1].ToLower().Contains("desc")))
                    {
                        throw new Exception("Invalid sort parameter. Incorrect sort order:" + component);
                    }
                }
            }
            IQueryable<Customer> sortedResults;
            if (string.IsNullOrWhiteSpace(listParameters.OrderBy))
            {
                sortedResults = dbContext.Customers;
            }
            else
            {
                sortedResults = dbContext.Customers
                  .OrderBy(listParameters.OrderBy);
            }

            if (!string.IsNullOrWhiteSpace(listParameters.LastName))
            {
                sortedResults = sortedResults
                    .Where(x => x.LastName.ToLower() == listParameters.LastName.Trim().ToLower());
            }

            if (!string.IsNullOrWhiteSpace(listParameters.Email))
            {
                sortedResults = sortedResults
                    .Where(x => x.EmailAddress.ToLower() == listParameters.Email.Trim().ToLower());
            }

            if (!string.IsNullOrWhiteSpace(listParameters.SearchTerm))
            {
                sortedResults = sortedResults
                    .Where(x => x.FirstName.ToLower().Contains(listParameters.SearchTerm.ToLower()) ||
                                x.LastName.ToLower().Contains(listParameters.SearchTerm.ToLower()) ||
                                x.EmailAddress.ToLower().Contains(listParameters.SearchTerm.ToLower()));
            }

            return sortedResults
            .Skip((Convert.ToInt16(listParameters.Page) - 1) * Convert.ToInt32(listParameters.Take))
            .Take(Convert.ToInt16(listParameters.Take))
            .ToList();

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
