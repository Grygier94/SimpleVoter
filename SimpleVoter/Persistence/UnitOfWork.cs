using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using SimpleVoter.Core;
using SimpleVoter.Core.Repositories;
using SimpleVoter.Persistence.Repositories;

namespace SimpleVoter.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IPollRepository Polls { get; }
        public IAnswerRepository Answers { get; }
        public IUserRepository Users { get; }
        public IDailyStatisticsRepository DailyStatistics { get; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Polls = new PollRepository(_context);
            Answers = new AnswerRepository(_context);
            Users = new UserRepository(_context);
            DailyStatistics = new DailyStatisticsRepository(_context);
        }

        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}