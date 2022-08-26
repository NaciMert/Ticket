using Microsoft.EntityFrameworkCore;
using Ticket.Core.Common;
using Ticket.Data;
using Ticket.Data.Entities;
using Ticket.Models;

namespace Ticket.Service
{
    public class UserService
    {
        private readonly TicketDbContext _ticketDbContext;
        public UserService(TicketDbContext ticketDbContext)
        {
            _ticketDbContext = ticketDbContext;
        }

        public async Task<Result<List<UserSummary>>> GetUserSummary()
        {
            var vResult = await _ticketDbContext.Users
                .Select(cm => new UserSummary
                {
                    Id = cm.Id,
                    UserNameSurname = cm.UserNameSurname,
                    UserPhone = cm.UserPhone,
                    UserEmail = cm.UserEmail,

                }).ToListAsync();

            return Result<List<UserSummary>>.PrepareSuccess(vResult);
        }

        public async Task<Result<UserSummary>> GetUserById(int userId)
        {

            var vUser = await _ticketDbContext.Users
                .Where(cm => cm.Id == userId)
                .Select(cm => new UserSummary()
                {
                    Id = cm.Id,
                    UserNameSurname = cm.UserNameSurname,
                    UserEmail = cm.UserEmail,
                    UserPhone = cm.UserPhone,
                    Role = cm.Role


                }).FirstOrDefaultAsync();

            if (vUser == null)
                return Result<UserSummary>.PrepareFailure("Kayıt Yok");

            return Result<UserSummary>.PrepareSuccess(vUser);

        }

        public async Task<Result<LoginSummary>> UserLogin(LoginInfo info)
        {

             try
            {
                var vCustomer = await _ticketDbContext.Users
               .Where(cs => cs.UserEmail == info.UserEmail && cs.UserPassword == info.UserPassword && cs.CustomerId == null)
               .Select(cs => new LoginSummary
               {
                   Id = cs.Id,
                   UserEmail = cs.UserEmail                 
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

        public async Task<Result<User>> AddUser(UserInfo userInfo)
        {

            var vResult = new User();

            vResult.Id = userInfo.Id;
            vResult.UserNameSurname = userInfo.UserNameSurname;
            vResult.UserEmail = userInfo.UserEmail;
            vResult.UserPhone = userInfo.UserPhone;
            vResult.UserPassword = userInfo.UserPassword;
            vResult.Role = userInfo.Role;
            vResult.CustomerId=userInfo.CustomerId;

            _ticketDbContext.Users.Add(vResult);

            await _ticketDbContext.SaveChangesAsync();

            return Result<User>.PrepareSuccess(vResult);
        }

        public async Task<Result<User>> UpdateUser(UserInfo userInfo)
        {

            var vUpdateUser = await _ticketDbContext.Users.Where(x => x.Id == userInfo.Id).FirstOrDefaultAsync();

            _ticketDbContext.Users.Attach(vUpdateUser);

            vUpdateUser.UserNameSurname = userInfo.UserNameSurname;
            vUpdateUser.UserPhone = userInfo.UserPhone;
            vUpdateUser.UserEmail = userInfo.UserEmail;
            vUpdateUser.UserPassword = userInfo.UserPassword;
            vUpdateUser.Role = userInfo.Role;

            await _ticketDbContext.SaveChangesAsync();

            return Result<User>.PrepareSuccess(vUpdateUser);
        }

        public async Task<Result> DeleteUser(int id)
        {
            var vResult = await _ticketDbContext.Users.FirstOrDefaultAsync(cm => cm.Id == id);

            if (vResult == null)
                return Result.PrepareFailure("Kayıt yok");

            _ticketDbContext.Remove(vResult);
            await _ticketDbContext.SaveChangesAsync();
            return Result.PrepareSuccess();

        }
    }
}
