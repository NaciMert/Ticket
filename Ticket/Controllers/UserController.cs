using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ticket.Data.Entities;
using Ticket.Models;
using Ticket.Service;

namespace Ticket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _ticketUser;
        public UserController(UserService ticketService)
        {
            _ticketUser = ticketService;

        }

        [HttpGet("UserSummary")]
        public async Task<IActionResult> GetUserSummary()
        {
            var vResult = await _ticketUser.GetUserSummary();

            return Ok(vResult);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetUserById(int userId)
        {
            var vResult = await _ticketUser.GetUserById(userId);

            return Ok(vResult);
        }

        [HttpPost("UserLogin")]
        public async Task<IActionResult> UserLogin(LoginInfo info)
        {
            var vResult = await _ticketUser.UserLogin(info);

            return Ok(vResult);
        }

        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser([FromBody] UserInfo userInfo)
        {
            var vResult = await _ticketUser.AddUser(userInfo);

            return Ok(vResult);
        }

        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser([FromBody] UserInfo userInfo)
        {
            var vResult = await _ticketUser.UpdateUser(userInfo);

            return Ok(vResult);
        }

        [HttpDelete("DeleteUser")]

        public async Task<IActionResult> DeleteUser(int id)
        {
            var vResult = await _ticketUser.DeleteUser(id);

            return Ok(vResult);
        }
    }
}