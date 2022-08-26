using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Core.Common;
using Ticket.Data.Entities;

namespace Ticket.Data.Configuration
{
    public class RequestConfiguration : IEntityTypeConfiguration<Request>
    {
        public void Configure(EntityTypeBuilder<Request> entity)
        {
            entity.ToTable("Tbl_Request");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");

            entity.Property(e => e.Status)
                .HasConversion(v => Convert.ToByte(v),
                v => (RequestStatuies)Enum.Parse(typeof(RequestStatuies), v.ToString()));
        }
       
    }


}

