using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ticket.Core.Common;
using Ticket.Data;
using Ticket.Models;
using Ticket.Service;

namespace Ticket.Controllers
{
    [Route("api/CustomerRequest")]
    [ApiController]
    public class CustomerRequestController : ControllerBase
    {
       
        private readonly CustomerRequestService _ticketRequest;
        public CustomerRequestController(CustomerRequestService ticketService)
        {
            _ticketRequest = ticketService;

        }

        [HttpGet("CustomerRequestSummary")]
        public async Task<IActionResult> GetCustomerRequestSummary()
        {
            var vResult=await _ticketRequest.GetCustomerRequestSummary();         
        
            return Ok(vResult);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetCustomerRequestById(int requestId)
        {
            var vResult = await _ticketRequest.GetCustomerRequestById(requestId);
            return Ok(vResult);
        }

        [HttpGet("GetByCustomerById")]
        public async Task<IActionResult> GetRequestByCustomerId(int customerId)
        {
            var vResult = await _ticketRequest.GetRequestByCustomerId(customerId);
            return Ok(vResult);
        }

        [HttpPost("AddRequest")]
        public async Task<IActionResult> AddRequest([FromBody] CustomerRequestInfo requestInfo)
        {
            var vResult = await _ticketRequest.AddRequest(requestInfo);

            return Ok(vResult);
        }
        [HttpPut("UpdateRequest")]
        public async Task<IActionResult> UpdateRequest([FromBody] CustomerRequestInfo requestInfo) 
        {
            var vResult = await _ticketRequest.UpdateRequest(requestInfo);
            return Ok(vResult);
        }

        [HttpDelete("DeleteCustomerRequest")]

        public async Task<IActionResult> DeleteCustomerProject(int id)
        {
            var vResult = await _ticketRequest.DeleteRequest(id);
            return Ok(vResult);
        }



    }
}
