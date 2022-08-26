using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ticket.Models;
using Ticket.Service;

namespace Ticket.Controllers
{
    [Route("api/UserRequest")]
    [ApiController]
    public class UserRequestController : ControllerBase
    {
        private readonly UserRequestService _userRequestService;

        public UserRequestController(UserRequestService userRequestService)
        {

            _userRequestService = userRequestService;
        }

        [HttpGet("UserRequestSummary")]
        public async Task<IActionResult> GetUserRequestSummary()
        {
            var vResult = await _userRequestService.GetUserRequestSummary();

            return Ok(vResult);
        }

        [HttpGet("GetUserRequestInfo/{requestId}")]
        public async Task<IActionResult> GetUserRequestInfo( int requestId)
        {
            var vResult = await _userRequestService.GetUserRequestInfo(requestId);
            return Ok(vResult);
        }

        [HttpPost("AddUserRequest")]
        public async Task<IActionResult> AddUserRequest([FromBody] UserRequestInfo userRequestInfo, int userId)
        {
            var vResult = await _userRequestService.AddUserRequest(userRequestInfo, userId);
            return Ok(vResult);
        }

        [HttpPut("UpdateUserRequest")]
        public async Task<IActionResult> UpdateUserRequest([FromBody] UserRequestInfo userRequestInfo, int userId)
        {
            var vResult = await _userRequestService.UpdateUserRequest(userRequestInfo, userId);
            return Ok(vResult);
        }

        [HttpGet("GetUserRequestSummary/{userId}")]
        public async Task<IActionResult> UserRequestSummary(int userId)
        {
            var vResult = await _userRequestService.GetUserRequestSummary( userId);
            return Ok(vResult);
        }
    }
}
