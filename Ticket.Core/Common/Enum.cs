using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticket.Core.Common
{
    public enum RequestStatuies
    {
        waiting = 0,
        working = 1,
        upgrading = 2,
        runningtest = 3,    
        finishing = 4,  
    }

    
}
