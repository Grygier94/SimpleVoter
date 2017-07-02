using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using SimpleVoter.Core.Models;

namespace SimpleVoter.Persistence.EntityConfigurations
{
    public class PollConfiguration : EntityTypeConfiguration<Poll>
    {
        public PollConfiguration()
        {
            Property(p => p.Question)
                .HasMaxLength(5000);
        }
    }
}