using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;
using SimpleCrm.WebApi.Models;
using System;
using System.Globalization;
using System.Linq;
using static Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary;

namespace SimpleCrm.WebApi.ApiControllers
{
    [Route("api/customers")]
    public class CustomerController : Controller
    {
        private readonly ICustomerData _customerData;
        private readonly LinkGenerator _linkGenerator;

        public CustomerController(ICustomerData customerData, LinkGenerator linkGenerator)
        {
            _customerData = customerData;
            _linkGenerator = linkGenerator;
        }
        /// <summary>
        /// Gets all customers visible in the account of the current user
        /// </summary>
        /// <returns></returns>
        [HttpGet("", Name = "GetCustomers")] //  ./api/customers
        public IActionResult GetAll([FromQuery]int page = 1, [FromQuery]int take = 50)
        {
            var customers = _customerData.GetAll(page - 1, take + 1, "");
            var pagination = new PaginationModel
            {
                Next = take + 1 == customers.Count ? GetCustomerResourceUri(page + 1, take) : null,
                Previous = page <= 1 ? null : GetCustomerResourceUri(page - 1, take)
            };
            customers.RemoveAt(customers.Count - 1);
            var models = customers.Select(c => new CustomerDisplayViewModel(c));

            var next = GetCustomerResourceUri(page + 1, take);
            var previous = GetCustomerResourceUri(page - 1, take);
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(pagination));

            return Ok(models); //200

        }

        private string GetCustomerResourceUri(int page, int take)
        {
            return _linkGenerator.GetPathByName(this.HttpContext, "GetCustomers",values: new { page = page, take = take });
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
        public IActionResult Create([FromBody] CustomerCreateViewModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return new ValidationFailedResult(ModelState);
            }

            var customer = new Customer
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                EmailAddress = model.EmailAddress,
                ContactMethod = model.PreferredContactMethod
            };

            _customerData.Add(customer);
            _customerData.Commit();
            return Ok(new CustomerDisplayViewModel(customer)); // 201  ToDo: generate a link
        }
        [HttpPut("{id}")] //  ./api/customers/:id
        public IActionResult Update(int id, [FromBody] CustomerUpdateViewModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return new ValidationFailedResult(ModelState);
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
