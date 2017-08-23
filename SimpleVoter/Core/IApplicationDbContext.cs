using System.Data.Entity;
using SimpleVoter.Core.Models;

namespace SimpleVoter.Core
{
    public interface IApplicationDbContext
    {
        IDbSet<Poll> Polls { get; set; }
        IDbSet<Answer> Answers { get; set; }
        IDbSet<ApplicationUser> Users { get; set; }
        IDbSet<DailyStatistics> DailyStatistics { get; set; }
    }
}