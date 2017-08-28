using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using SimpleVoter.Core;
using SimpleVoter.Core.Models;
using SimpleVoter.Persistence.EntityConfigurations;

namespace SimpleVoter.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
    {
        public IDbSet<Poll> Polls { get; set; }
        public IDbSet<Answer> Answers { get; set; }
        public IDbSet<DailyStatistics> DailyStatistics { get; set; }
        public IDbSet<UniqueVisitor> UniqueVisitors { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new PollConfiguration());
            modelBuilder.Configurations.Add(new AnswerConfiguration());
            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new DailyStatisticsConfiguration());
            modelBuilder.Configurations.Add(new UniqueVisitorsConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}