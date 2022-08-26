using Microsoft.EntityFrameworkCore;
using Ticket.Core.Common;
using Ticket.Data;
using Ticket.Data.Entities;
using Ticket.Models;
using Ticket.Projects;

namespace Ticket.Service
{
    public class ProjectService
    {
        private readonly TicketDbContext _ticketDbContext;
        public ProjectService(TicketDbContext ticketDbContext)
        {
            _ticketDbContext = ticketDbContext;
        }

        public async Task<Result<List<ProjectSummary>>> GetProjectSummary()
        {
            try
            {
                var vResult = await (from p in _ticketDbContext.Projects
                    join jp in _ticketDbContext.Projects on p.ParentId equals jp.Id
                    select new ProjectSummary()
                    {
                        Id = p.Id,
                        Definition = p.Definition,
                        ParentId = p.ParentId,
                        ParentDefinition = jp.Definition,
                    }).ToListAsync();

                return Result<List<ProjectSummary>>.PrepareSuccess(vResult);
            }
            catch (Exception ex)
            {
                Console.WriteLine(" ", ex);
                return  Result<List<ProjectSummary>>.PrepareFailure("Kayıt yok");
            }

        }

        public async Task<Result<ProjectSummary>> GetProjectById(int projectId)
        {

            var vProject = await (from p in _ticketDbContext.Projects
                                 join jp in _ticketDbContext.Projects on p.ParentId equals jp.Id
                                 where p.Id == projectId
                                 select new ProjectSummary()
                                 {
                                     Id = p.Id,
                                     Definition = p.Definition,
                                     ParentId = p.ParentId,
                                     ParentDefinition = jp.Definition,
                                 }).FirstOrDefaultAsync();

            if (vProject == null)
                return Result<ProjectSummary>.PrepareFailure("Kayıt yok");

            return Result<ProjectSummary>.PrepareSuccess(vProject);

        }

        public async Task<Result<Project>> AddProject(ProjectInfo projectInfo)
        {

            var vResult = new Project();

            vResult.Definition = projectInfo.Definition;
            vResult.ParentId = projectInfo.ParentId;
            

            _ticketDbContext.Projects.Add(vResult);

            await _ticketDbContext.SaveChangesAsync();

            return Result<Project>.PrepareSuccess(vResult);
        }

        public async Task<Result<Project>> UpdateProject(ProjectInfo projectInfo)
        {

            var vUpdateProject = await _ticketDbContext.Projects.Where(x => x.Id == projectInfo.Id).FirstOrDefaultAsync();

            _ticketDbContext.Projects.Attach(vUpdateProject);

            vUpdateProject.Id = projectInfo.Id;
            vUpdateProject.Definition = projectInfo.Definition;
            vUpdateProject.ParentId = projectInfo.ParentId;
         

            await _ticketDbContext.SaveChangesAsync();

            return Result<Project>.PrepareSuccess(vUpdateProject);
        }

        public async Task<Result> DeleteProject(int id)
        {
            var vResult = await _ticketDbContext.Projects.FirstOrDefaultAsync(p => p.Id == id);

            if (vResult == null)
                return Result.PrepareFailure("Kayıt yok");

            _ticketDbContext.Remove(vResult);
            await _ticketDbContext.SaveChangesAsync();
            return Result.PrepareSuccess();
        }
    }
}
