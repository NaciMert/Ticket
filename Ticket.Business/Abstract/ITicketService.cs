using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Data.Entities;

namespace Ticket.Business.Abstract
{
    public interface ITicketService
    {
        List<Request> GetAllRequest();
        Request GetRequestById(int id); 
        Request CreateRequest(Request request); 
        Request UpdateRequest(Request request); 
        void DeleteRequest(int id); 
    }
}
