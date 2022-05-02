using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Policy = "ApiUser")]
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
        public IActionResult GetAll([FromQuery]CustomerListParameters resourceParameters)
        {
            if (resourceParameters.Page == null)
            {
                resourceParameters.Page = 1;
            }
            if (resourceParameters.Take > 50 ||
                resourceParameters.Take < 1 ||
                resourceParameters.Page < 1)
            {
                 return BadRequest();
            }
            var customers = _customerData.GetAll(resourceParameters);
            var pagination = new PaginationModel();
            if (resourceParameters.Take == customers.Count)
            {
                pagination.Next = GetCustomerResourceUri(resourceParameters, 1);
            }
            pagination.Previous = resourceParameters.Page <= 1 ?
                null : GetCustomerResourceUri(resourceParameters, -1);

            var models = customers.Select(c => new CustomerDisplayViewModel(c));

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(pagination));

            return Ok(models); //200

        }
        private string GetCustomerResourceUri(CustomerListParameters listParameters, int pageAdjust)
        {
            if (listParameters.Page + pageAdjust <= 0)
                return null;


            return _linkGenerator.GetPathByName(this.HttpContext, "GetCustomers",
                    values: new { 
                                    page = listParameters.Page + pageAdjust, 
                                    take = listParameters.Take,
                                    orderBy = listParameters.OrderBy,
                                    LastName = listParameters.LastName,
                                    Email = listParameters.Email,
                                    SearchTerm = listParameters.SearchTerm

                    });
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
