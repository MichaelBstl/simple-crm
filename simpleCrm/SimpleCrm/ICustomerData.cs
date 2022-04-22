using System.Collections.Generic;

namespace SimpleCrm
{
    public interface ICustomerData
    {
        IEnumerable<Customer> GetAll();
        Customer Get(int id);
        List<Customer> GetAll(CustomerListParameters listParameters);
        void Add(Customer customer);
        void Delete(Customer customer);
        void Update(Customer customer);
        void Commit();
    }
}
