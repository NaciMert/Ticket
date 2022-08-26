using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Data.Configuration;
using Ticket.Data.Entities;

namespace Ticket.Data
{
    public class TicketDbContext : DbContext
    {
        public TicketDbContext()
        {

        }

        public TicketDbContext(DbContextOptions<TicketDbContext> options) : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProjectConfiguration).Assembly);
        }

        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<Request> Requests { get; set; } = null!;
        public virtual DbSet<Project> Projects { get; set; } = null!;

    }
}
