using Microsoft.EntityFrameworkCore;
using Ticket.Core.Common;
using Ticket.Data;
using Ticket.Models;

namespace Ticket.Service
{
    public class UserRequestService
    {
        private readonly TicketDbContext _ticketDbContext;
        public UserRequestService(TicketDbContext ticketDbContext)
        {
            _ticketDbContext = ticketDbContext;
        }

        public async Task<Result<List<UserRequestSummary>>> GetUserRequestSummary()
        {
            var vResult = await _ticketDbContext.Requests
                .OrderBy(rq => rq.CreateDate)
                .ThenBy(rq => rq.Status)
                .Select(rq => new UserRequestSummary
                {
                    Id = rq.Id,
                    ProjectId = rq.ProjectId,
                    ModuleId = rq.ModuleId,
                    ErrorDescription = rq.ErrorDescription,
                    CustomerId = rq.CustomerId,
                    CreateDate = rq.CreateDate,
                    UpdateDate = rq.UpdateDate,
                    ErrorTitle = rq.ErrorTitle,
                    UserId = rq.UserId,
                    Status = rq.Status,

                })
                .ToListAsync();

            return Result<List<UserRequestSummary>>.PrepareSuccess(vResult);
        }

        public async Task<Result<UserRequestInfo>> GetUserRequestInfo(int requestId)
        {
                try
            {
                var vUser = await _ticketDbContext.Requests
                .Select(cm => new UserRequestInfo()

                {
                    Id = cm.Id,
                    ErrorDescription=cm.ErrorDescription,
                    CreateDate = cm.CreateDate,
                    UpdateDate = cm.UpdateDate,
                    Status = cm.Status,
                    UserId = cm.UserId, 
                    ProjectId = cm.ProjectId,
                    ModuleId=cm.ModuleId,   
                    CustomerId=cm.CustomerId,   
                    ErrorTitle=cm.ErrorTitle,   
                   

                }).FirstOrDefaultAsync();


                return Result<UserRequestInfo>.PrepareSuccess(vUser);

            }
            catch (Exception e)
            {

                return Result<UserRequestInfo>.PrepareFailure("Hata mesajı");
            }

        }


        public async Task<Result<List<UserRequestSummary>>> GetUserRequestSummary(int userId)
         {
            try
            {
                var vUser = _ticketDbContext.Users.FirstOrDefault(cm => cm.Id == userId);

                var vUserRequest = await _ticketDbContext.Requests
                    .Where(cm => (vUser.Role == "Admin" || cm.UserId == userId))
                    .Select(cm => new UserRequestSummary()
                    {
                        Id = cm.Id,
                        ProjectId = cm.ProjectId,
                        ModuleId = cm.ModuleId,
                        CustomerId = cm.CustomerId,
                        UserId = cm.UserId,
                        ErrorDescription = cm.ErrorDescription,
                        CreateDate = cm.CreateDate,
                        UpdateDate = cm.UpdateDate,
                        ErrorTitle = cm.ErrorTitle,
                        Status = cm.Status,

                    })
                    .ToListAsync();

                return Result<List<UserRequestSummary>>.PrepareSuccess(vUserRequest);
            }
            catch (Exception e)
            {

                return Result<List<UserRequestSummary>>.PrepareFailure(e.Message);
            }
        }

        public async Task<Result> AddUserRequest(UserRequestInfo userRequestInfo, int userId)
        {
            var vUser = _ticketDbContext.Users.FirstOrDefault(cm => cm.Id == userId);

            if (vUser.Role != "Admin")
            {
                return Result.PrepareFailure("");
            }

            var vAddRequest = await _ticketDbContext.Requests.Where(x => x.Id == userRequestInfo.Id).FirstOrDefaultAsync();
            if (vAddRequest == null)
            {
                return Result.PrepareFailure("Tüm değerleri doldurmanız gerekiyor!");
            }


            _ticketDbContext.Requests.Attach(vAddRequest);

            vAddRequest.Id = userRequestInfo.Id;
            vAddRequest.ProjectId = userRequestInfo.ProjectId;
            vAddRequest.ModuleId = userRequestInfo.ModuleId;
            vAddRequest.CustomerId = userRequestInfo.CustomerId;
            vAddRequest.CreateDate = userRequestInfo.CreateDate;
            vAddRequest.UserId = userRequestInfo.UserId;
            vAddRequest.UpdateDate = DateTime.Now;
            vAddRequest.Status = userRequestInfo.Status;
            vAddRequest.ErrorTitle = userRequestInfo.ErrorTitle;
            vAddRequest.ErrorDescription = userRequestInfo.ErrorDescription;

            await _ticketDbContext.SaveChangesAsync();

            return Result.PrepareSuccess();

        }


        public async Task<Result> UpdateUserRequest(UserRequestInfo userRequestInfo, int userId)
        {
            var vUser = _ticketDbContext.Users.FirstOrDefault(cm => cm.Id == userId);

            if (vUser.Role != "Admin")
            {
                return Result.PrepareFailure("");
            }

            var vUpdateRequest = await _ticketDbContext.Requests.Where(x => x.Id == userRequestInfo.Id).FirstOrDefaultAsync();
            if (vUpdateRequest==null)
            {
                return Result.PrepareFailure("Tüm değerleri doldurmanız gerekiyor!");
            }


            _ticketDbContext.Requests.Attach(vUpdateRequest);

            vUpdateRequest.Id = userRequestInfo.Id;
            vUpdateRequest.ProjectId = userRequestInfo.ProjectId;
            vUpdateRequest.ModuleId = userRequestInfo.ModuleId;
            vUpdateRequest.CustomerId = userRequestInfo.CustomerId;
            vUpdateRequest.CreateDate = userRequestInfo.CreateDate;
            vUpdateRequest.UserId = userRequestInfo.UserId;
            vUpdateRequest.UpdateDate = DateTime.Now;
            vUpdateRequest.Status = userRequestInfo.Status;
            vUpdateRequest.ErrorTitle = userRequestInfo.ErrorTitle;
            vUpdateRequest.ErrorDescription = userRequestInfo.ErrorDescription;

            await _ticketDbContext.SaveChangesAsync();

            return Result.PrepareSuccess();

        }
    }
}
