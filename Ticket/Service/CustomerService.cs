using Microsoft.EntityFrameworkCore;
using Ticket.Core.Common;
using Ticket.Data;
using Ticket.Data.Entities;
using Ticket.Models;

namespace Ticket.Service
{
    public class CustomerService
    {
        private readonly TicketDbContext _ticketDbContext;
        public CustomerService(TicketDbContext ticketDbContext)
        {
            _ticketDbContext = ticketDbContext;
        }

        public async Task<Result<List<CustomerSummary>>> GetCustomerSummary()
        {
            var vResult = await _ticketDbContext.Customers

                .Select(cs => new CustomerSummary
                {
                    Id = cs.Id,
                    Name= cs.Name,
                    Email = cs.Email,
                    Phone = cs.Phone,
                }).ToListAsync();

            return Result<List<CustomerSummary>>.PrepareSuccess(vResult);
        }

        public async Task<Result<CustomerSummary>> GetCustomerById(int customerId)
        {

            var vCustomer = await _ticketDbContext.Customers
                .Where(cs => cs.Id == customerId)
                .Select(cs => new CustomerSummary()
                {
                    Id = cs.Id,
                    Name = cs.Name,
                    Email = cs.Email,
                    Phone = cs.Phone,
                   

                }).FirstOrDefaultAsync();

            if (vCustomer == null)
                return Result<CustomerSummary>.PrepareFailure("Kayıt yok");

            return Result<CustomerSummary>.PrepareSuccess(vCustomer);

        }

        public async Task<Result<LoginSummary>> CustomerLogin(LoginInfo info)
        {
            try
            {
                var vCustomer = await _ticketDbContext.Users
               .Where(cs => cs.UserEmail == info.UserEmail && cs.UserPassword == info.UserPassword && cs.CustomerId != null)
               .Select(cs => new LoginSummary
               {
                   Id = cs.Id,
                   UserEmail = cs.UserEmail,
                   CustomerId = cs.CustomerId,
                   
               }).FirstOrDefaultAsync();

                if (vCustomer == null)
                    return Result<LoginSummary>.PrepareFailure("Kullanıcı bulunamadı");

                return Result<LoginSummary>.PrepareSuccess(vCustomer);
            }
            catch (Exception ex)
            {

                return Result<LoginSummary>.PrepareFailure(ex.Message);

            }
        }



        public async Task<Result<Customer>> AddCustomer(CustomerInfo customerInfo)
        {

            var vResult = new Customer();

            vResult.Name = customerInfo.Name;
            vResult.Email = customerInfo.Email;
            vResult.Phone = customerInfo.Phone;
         

            _ticketDbContext.Customers.Add(vResult);

            await _ticketDbContext.SaveChangesAsync();

            return Result<Customer>.PrepareSuccess(vResult);
        }

        public async Task<Result<Customer>> UpdateCustomer(CustomerInfo customerInfo)
        {

            var vUpdateCustomer = await _ticketDbContext.Customers.Where(x => x.Id == customerInfo.Id).FirstOrDefaultAsync();

            _ticketDbContext.Customers.Attach(vUpdateCustomer);

            vUpdateCustomer.Id = customerInfo.Id;
            vUpdateCustomer.Name = customerInfo.Name;
            vUpdateCustomer.Email = customerInfo.Email;
            vUpdateCustomer.Phone = customerInfo.Phone;
            
            await _ticketDbContext.SaveChangesAsync();

            return Result<Customer>.PrepareSuccess(vUpdateCustomer);
        }

        public async Task<Result> DeleteCustomer(int id)
        {
            var vResult = await _ticketDbContext.Customers.FirstOrDefaultAsync(cs => cs.Id == id);

            if (vResult == null)
                return Result.PrepareFailure("Kayıt yok");

            _ticketDbContext.Remove(vResult);
            await _ticketDbContext.SaveChangesAsync();
            return Result.PrepareSuccess();
        }
    }
}
