using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticket.Data.Entities
{
    public class Project
    {
        public int Id { get; set; }

        public string Definition { get; set; }

        public int ParentId { get; set; }
        

       

    }
}
