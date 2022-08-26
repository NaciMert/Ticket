using Microsoft.EntityFrameworkCore;
using Ticket.Core.Common;
using Ticket.Data;
using Ticket.Data.Entities;
using Ticket.Models;

namespace Ticket.Service
{
    public class CustomerRequestService
    {
        private readonly TicketDbContext _ticketDbContext;
        public CustomerRequestService(TicketDbContext ticketDbContext)
        {
            _ticketDbContext = ticketDbContext;
        }

        public async Task<Result<List<CustomerRequestSummary>>> GetCustomerRequestSummary()
        {
            var vResult = await _ticketDbContext.Requests
                .OrderBy(rq => rq.CreateDate)
                .ThenBy(rq => rq.Status)
                .Select(rq => new CustomerRequestSummary
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
                    Status=rq.Status,

                })
                .ToListAsync();

            return Result<List<CustomerRequestSummary>>.PrepareSuccess(vResult);
        }

        public async Task<Result<CustomerRequestSummary>> GetCustomerRequestById(int requestId)
        {

            var vProject = await _ticketDbContext.Requests
                .Where(rq => rq.Id == requestId)
                .Select(rq => new CustomerRequestSummary()
                {
                    Id = rq.Id,
                    ProjectId=rq.ProjectId,
                    ModuleId=rq.ModuleId,
                    ErrorDescription = rq.ErrorDescription,
                    CustomerId=rq.CustomerId,
                    CreateDate=rq.CreateDate,
                    UserId=rq.UserId,
                    UpdateDate=rq.UpdateDate,
                    Status=rq.Status,
                    ErrorTitle=rq.ErrorTitle,
                   

                }).FirstOrDefaultAsync();

            if (vProject == null)
                return Result<CustomerRequestSummary>.PrepareFailure("Kayıt yok");

            return Result<CustomerRequestSummary>.PrepareSuccess(vProject);

        }


        public async Task<Result<List<CustomerRequestSummary>>> GetRequestByCustomerId(int customerId)
        {
            try
            {
                var vCustomerRequest = await _ticketDbContext.Requests.Where(cm =>cm.CustomerId == customerId)
                    .Select(cm => new CustomerRequestSummary()
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
                        Status=cm.Status,
                        
                    })
                    .ToListAsync();

                if (vCustomerRequest == null)
                    return Result<List<CustomerRequestSummary>>.PrepareFailure("Kayıt yok");

                return Result<List<CustomerRequestSummary>>.PrepareSuccess(vCustomerRequest);
            }
            catch (Exception e)
            {

                return Result<List<CustomerRequestSummary>>.PrepareFailure(e.Message);
            }
        }


        public async Task<Result> AddRequest(CustomerRequestInfo requestInfo)
        {

            var vResult = new Request();

           
            vResult.ProjectId = requestInfo.ProjectId;
            vResult.ModuleId = requestInfo.ModuleId;
            vResult.ErrorDescription = requestInfo.ErrorDescription;
            vResult.CustomerId = requestInfo.CustomerId;
            vResult.CreateDate = DateTime.Now;
            vResult.ProjectId = requestInfo.ProjectId;
            vResult.ErrorTitle = requestInfo.ErrorTitle;
            vResult.Status = RequestStatuies.waiting; 
            

            _ticketDbContext.Requests.Add(vResult);

            await _ticketDbContext.SaveChangesAsync();

            return Result.PrepareSuccess();
        }

        public async Task<Result<Request>> UpdateRequest(CustomerRequestInfo requestInfo)
        {

               var vUpdateRequest = await _ticketDbContext.Requests.Where(x => x.Id == requestInfo.Id).FirstOrDefaultAsync();

            _ticketDbContext.Requests.Attach(vUpdateRequest);

          
            vUpdateRequest.ProjectId = requestInfo.ProjectId;
            vUpdateRequest.ModuleId = requestInfo.ModuleId;
            vUpdateRequest.ErrorDescription = requestInfo.ErrorDescription;
            vUpdateRequest.CustomerId = requestInfo.CustomerId;
            vUpdateRequest.CreateDate = DateTime.Now;
            vUpdateRequest.UpdateDate = DateTime.Now;
            vUpdateRequest.ErrorTitle = requestInfo.ErrorTitle;
          


            await _ticketDbContext.SaveChangesAsync();

            return Result<Request>.PrepareSuccess(vUpdateRequest);
        }

        public async Task<Result> DeleteRequest(int id)
        {
            var vResult = await _ticketDbContext.Requests.FirstOrDefaultAsync(rq => rq.Id == id);

            if (vResult == null)
                return Result.PrepareFailure("Kayıt yok");

            _ticketDbContext.Remove(vResult);
            await _ticketDbContext.SaveChangesAsync();
            return Result.PrepareSuccess();
        }

      

    }


}
