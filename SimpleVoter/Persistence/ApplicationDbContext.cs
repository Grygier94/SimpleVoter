using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using SimpleVoter.Core.Models;
using SimpleVoter.Persistence.EntityConfigurations;

namespace SimpleVoter.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Poll> Polls { get; set; }
        public DbSet<Answer> Answers { get; set; }

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
            base.OnModelCreating(modelBuilder);
        }
    }
}