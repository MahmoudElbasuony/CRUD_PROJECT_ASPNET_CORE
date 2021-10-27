using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APP.BusinessLogic.Interfaces;
using APP.CRUDTask.DTO;
using APP.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CRUDTask.Controllers
{
    [Route("api/[controller]")]
    public class CustomerController : Controller
    {
        private readonly ICustomerService _CustomerService;
        private readonly ILogger<CompanyController> _Logger;
        public CustomerController(ICustomerService customerService, ILogger<CompanyController> logger)
        {
            _CustomerService = customerService;
            _Logger = logger;
        }
        // GET: api/<controller>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery(Name = "pagenumber")] int pageNumber = 0, [FromQuery(Name = "pagenumber")] int pageSize = 100)
        {
            try
            {
                var list = (await _CustomerService.GetAllCustomers(pageNumber, pageSize)).Select(e => CreateViewDtoFromEntity(e)).ToList();
                return Ok(list);
            }
            catch (Exception e)
            {
                _Logger.LogError(e.ToString());
                return Problem("Couldn't reterive customers", statusCode: 500);
            }
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var customer = await _CustomerService.GetCustomerById(id);
                return Ok(CreateViewDtoFromEntity(customer));
            }
            catch (Exception e)
            {
                _Logger.LogError(e.ToString());
                return Problem("Couldn't reterive customer!", statusCode: 500);
            }
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddCustomerDTO customer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var new_customer = await _CustomerService.CreateCustomer(customer.Name, customer.MobileNo, customer.Job, customer.BirthDate, customer.CompanyId, customer.Email);
                    return Ok(CreateViewDtoFromEntity(new_customer));
                }
                else
                    return BadRequest(ModelState);
            }
            catch (Exception e)
            {
                _Logger.LogError(e.ToString());
                return Problem("Couldn't create customer!", statusCode: 500);
            }
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateCustomerDTO customer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var updated_customer = await _CustomerService.UpdateCustomer(id, customer.Name, customer.MobileNo, customer.Job, customer.BirthDate, customer.CompanyId, customer.Email);
                    return Ok(CreateViewDtoFromEntity(updated_customer));
                }
                else
                    return BadRequest(ModelState);
            }
            catch (Exception e)
            {
                _Logger.LogError(e.ToString());
                return Problem("Couldn't create customer!", statusCode: 500);
            }
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _CustomerService.DeleteCustomer(id);
                return Ok();
            }
            catch (Exception e)
            {
                _Logger.LogError(e.ToString());
                return Problem("Couldn't delete customer!", statusCode: 500);
            }
        }

        private ViewCustomerDTO CreateViewDtoFromEntity(Customer customer)
        {
            if (customer == null)
                return null;
            return new ViewCustomerDTO
            {
                BirthDate = customer.BirthDate,
                CompanyName = customer.Company.Name,
                CompanyId = customer.Company.ID,
                Job = customer.Job,
                MobileNo = customer.MobileNo,
                Name = customer.Name,
                Email = customer.Email,
                ID = customer.ID
            };
        }
    }
}
