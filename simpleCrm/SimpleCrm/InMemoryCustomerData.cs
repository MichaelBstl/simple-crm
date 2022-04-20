using System.Collections.Generic;
using System.Linq;

namespace SimpleCrm
{
    public class InMemoryCustomerData : ICustomerData
    {
        static IList<Customer> _customers; //not thread safe - only ok for development, single user

        static InMemoryCustomerData()
        {
            _customers = new List<Customer>
                  {
                      new Customer { Id =1, FirstName ="Bob", LastName = "Jones", PhoneNumber = "555-555-2345", Status = CustomerStatus.Initial },
                      new Customer { Id =2, FirstName ="Jane", LastName = "Smith", PhoneNumber = "555-555-5256", Status = CustomerStatus.Partner },
                      new Customer { Id =3, FirstName ="Mike", LastName = "Doe", PhoneNumber = "555-555-8547", Status = CustomerStatus.Prospect },
                      new Customer { Id =4, FirstName ="Karen", LastName = "Jamieson", PhoneNumber = "555-555-9134", Status = CustomerStatus.Initial },
                      new Customer { Id =5, FirstName ="James", LastName = "Dean", PhoneNumber = "555-555-7245", Status = CustomerStatus.Prospect },
                      new Customer { Id =6, FirstName ="Michelle", LastName = "Leary", PhoneNumber = "555-555-3457", Status = CustomerStatus.Partner }
                  };
        }

        public Customer Get(int id)
        {
            return (from c in _customers where c.Id == id select c).FirstOrDefault();
        }

        public IEnumerable<Customer> GetAll()
        {
            var customer = _customers;
            return customer;
        }

        public void Add(Customer customer)
        {
            customer.Id = _customers.Max(a => a.Id) + 1;
            _customers.Add(customer);
        }
        public void Update(Customer customer)
        {
            int index = _customers.IndexOf((from cust in _customers where cust.Id == customer.Id select cust).FirstOrDefault());
            _customers[index] = customer;
            return;
        }
        public void Commit ()
        {

        }
        public List<Customer> GetAll(int pageIndex, int take, string orderBy)
        {
            List<Customer> customers = ((List<Customer>)(from cust in _customers select cust));
            return customers;
        }

        public void Delete(Customer customer)
        {
            int index = _customers.IndexOf((from cust in _customers where cust.Id == customer.Id select cust).FirstOrDefault());
            _customers.RemoveAt(index);
            return;
        }
    }
}