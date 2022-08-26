using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticket.Data.Entities
{
    public class User
    {
        public int Id { get; set; }

        public string UserNameSurname { get; set; }

        public string UserPhone { get; set; }

        public string UserEmail { get; set; }

        public string UserPassword { get; set; }

        public string? Role { get; set; }

        public int? CustomerId { get; set; }
    }
}
