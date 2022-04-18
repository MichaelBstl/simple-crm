using Microsoft.AspNetCore.Mvc;
using SimpleCrm.WebApi.Models;
using System;
using System.Globalization;
using System.Linq;

namespace SimpleCrm.WebApi.ApiControllers
{
    [Route("api/customers")]
    public class CustomerController : Controller
    {
        private readonly ICustomerData _customerData;

        public CustomerController(ICustomerData customerData)
        {
            _customerData = customerData;
        }
        /// <summary>
        /// Gets all customers visible in the account of the current user
        /// </summary>
        /// <returns></returns>
        [HttpGet("")] //  ./api/customers
        public IActionResult GetAll()
        {
            var customers = _customerData.GetAll(0, 50, "");
            var models = customers.Select(c => new CustomerDisplayViewModel(c));
            return Ok(models); //200

        }
        /// <summary>
        /// Retrieves a single customer by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")] //  ./api/customers/:id
        public IActionResult Get(int id)
        {
            var customer = _customerData.Get(id);
            if (customer == null)
            {
                return NotFound(); // 404
            }
            var models = new CustomerDisplayViewModel(customer);

            return Ok(models); // 200
        }
        [HttpPost("")] //  ./api/customers
        public IActionResult Create([FromBody] CustomerUpdateViewModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }
            /*            
                        if (!ModelState.IsValid)
                        {
                            return new ValidationFailedResult(ModelState); // ValidationFailedResult is causing an error
                        }
            */
            var customer = new Customer();
            if (customer == null)
            {
                return NotFound(); // 404
            }
            customer.FirstName = model.FirstName;
            customer.LastName = model.LastName;
            customer.PhoneNumber = model.PhoneNumber;
            customer.EmailAddress = model.EmailAddress;
            customer.ContactMethod = model.PreferredContactMethod;

            _customerData.Add(customer);
            _customerData.Commit();
            return Created("Id", model); // 201  ToDo: generate a link
        }
        [HttpPut("{id}")] //  ./api/customers/:id
        public IActionResult Update(int id, [FromBody] CustomerUpdateViewModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }
            var customer = _customerData.Get(id);
            if (customer == null)
            {
                return NotFound(); // 404
            }

            customer.FirstName = model.FirstName;
            customer.LastName = model.LastName;
            customer.PhoneNumber = model.PhoneNumber;
            customer.EmailAddress = model.EmailAddress;
            customer.ContactMethod = model.PreferredContactMethod;
            customer.OptInNewsletter = model.OptInNewsletter;
            customer.Status = model.Status;
            customer.Type = model.Type;

            _customerData.Update(customer);
            _customerData.Commit();
            return NoContent();  // 204
        }
        [HttpDelete("{id}")] //  ./api/customers/:id
        public IActionResult Delete(Customer customer)
        {
            var tempCustomer = _customerData.Get(customer.Id);
            if (tempCustomer == null)
            {
                return NotFound(); // 404
            }
            _customerData.Delete(customer);
            _customerData.Commit();
            return NoContent(); // 204
        }
    }
}
