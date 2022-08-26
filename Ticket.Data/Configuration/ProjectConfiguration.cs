using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Data.Entities;

namespace Ticket.Data.Configuration
{
    public class ProjectConfiguration:IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> entity)
        {
            entity.ToTable("Tbl_Project");
            entity.HasKey(e => e.Id);
           
            //entity.HasMany(e => e.Modules)
            //.WithOne()
            //.HasForeignKey(e => e.ParentId);
       
        }
    }
}
