using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ticket.Models;
using Ticket.Service;

namespace Ticket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
         
        private readonly CustomerService _ticketCustomer;
        public CustomerController(CustomerService ticketService)
        {
            _ticketCustomer = ticketService;

        }


        [HttpGet("CustomerSummary")]
        public async Task<IActionResult> GetCustomerSummary()
        {
            var vResult = await _ticketCustomer.GetCustomerSummary();

            return Ok(vResult);
        }

    
        [HttpGet("GetById")]
        public async Task<IActionResult> GetCustomerById(int customerId)
        {
            var vResult = await _ticketCustomer.GetCustomerById(customerId);
            return Ok(vResult);
        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginInfo info)
        {
            var vResult = await _ticketCustomer.CustomerLogin(info);

            return Ok(vResult);
        }

        [HttpPost("AddCustomer")]
        public async Task<IActionResult> AddCustomer([FromBody]CustomerInfo customerInfo)
        {
            var vResult = await _ticketCustomer.AddCustomer(customerInfo);

            return Ok(vResult);
        }

        [HttpPut("UpdateCustomer")]
        public async Task<IActionResult> UpdateCustomer([FromBody] CustomerInfo customerInfo)
        {
            var vResult = await _ticketCustomer.UpdateCustomer(customerInfo);

            return Ok(vResult);
        }

        [HttpDelete("DeleteCustomer")]

        public async Task<IActionResult> DeleteCompany(int id)
        {
            var vResult = await _ticketCustomer.DeleteCustomer(id);

            return Ok(vResult);
        }

    }
}

