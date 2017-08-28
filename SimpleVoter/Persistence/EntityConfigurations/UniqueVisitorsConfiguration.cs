using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using SimpleVoter.Core.Models;

namespace SimpleVoter.Persistence.EntityConfigurations
{
    public class UniqueVisitorsConfiguration : EntityTypeConfiguration<UniqueVisitor>
    {
        public UniqueVisitorsConfiguration()
        {
            Property(uv => uv.IpAdress)
                .HasMaxLength(45);
        }
    }
}