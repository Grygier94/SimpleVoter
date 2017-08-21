using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using SimpleVoter.Core.Models;

namespace SimpleVoter.Persistence.EntityConfigurations
{
    public class UserConfiguration:EntityTypeConfiguration<ApplicationUser>
    {
        public UserConfiguration()
        {
            HasMany(p => p.Polls)
                .WithOptional(u => u.User)
                .WillCascadeOnDelete(true);
        }
    }
}