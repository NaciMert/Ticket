using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ticket.Models;
using Ticket.Service;

namespace Ticket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly ProjectService _ticketProject;
        public ProjectController(ProjectService ticketService)
        {
            _ticketProject = ticketService;

        }

        [HttpGet("ProjectSummary")]
        public async Task<IActionResult> GetProjectSummary()
        {
            var vResult = await _ticketProject.GetProjectSummary();

            return Ok(vResult);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetProjectById(int projectId)
        {
            var vResult = await _ticketProject.GetProjectById(projectId);
            return Ok(vResult);
        }

        [HttpPost("AddProject")]
        public async Task<IActionResult> AddProject([FromBody] ProjectInfo projectInfo)
        {
            var vResult = await _ticketProject.AddProject(projectInfo);

            return Ok(vResult);
        }


        [HttpPut("UpdateProject")]
        public async Task<IActionResult> UpdateProject([FromBody] ProjectInfo projectInfo)
        {
            var vResult = await _ticketProject.UpdateProject(projectInfo);

            return Ok(vResult);
        }

        [HttpDelete("DeleteProject")]

        public async Task<IActionResult> DeleteProject(int id)
        {
            var vResult = await _ticketProject.DeleteProject(id);
            return Ok(vResult);
        }


    }
}
