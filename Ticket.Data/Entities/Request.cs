using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Core.Common;

namespace Ticket.Data.Entities
{
    public class Request
    {
        public int Id { get; set; }     
        public int? ProjectId { get; set; }   
        public int? ModuleId { get; set; }  
        public string? ErrorDescription { get; set; }
        public int? CustomerId { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? UserId { get; set; }
        public DateTime? UpdateDate { get; set; }    
        public RequestStatuies? Status { get; set; } 
        public string? ErrorTitle { get; set; }  
        

    }
}
