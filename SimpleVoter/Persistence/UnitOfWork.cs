using System;
using System.Collections.Generic;
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
        public IAnswerRepository Answer { get; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Polls = new PollRepository(_context);
            Answer = new AnswerRepository(_context);
        }

        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}