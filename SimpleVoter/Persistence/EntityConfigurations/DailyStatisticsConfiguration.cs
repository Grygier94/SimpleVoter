using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using SimpleVoter.Core.Models;

namespace SimpleVoter.Persistence.EntityConfigurations
{
    public class DailyStatisticsConfiguration : EntityTypeConfiguration<DailyStatistics>
    {
        public DailyStatisticsConfiguration()
        {
            HasKey(ds => ds.Id);

            Property(ds => ds.Date)
                .HasColumnType("date");

            Property(ds => ds.Users)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);

            Property(ds => ds.PublicPolls)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);

            Property(ds => ds.PrivatePolls)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
        }
    }
}