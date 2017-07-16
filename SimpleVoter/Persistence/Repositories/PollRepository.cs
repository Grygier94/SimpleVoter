using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using SimpleVoter.Core;
using SimpleVoter.Core.Models;
using SimpleVoter.Core.Repositories;

namespace SimpleVoter.Persistence.Repositories
{
    public class PollRepository : IPollRepository
    {
        protected readonly IApplicationDbContext Context;
        public PollRepository(IApplicationDbContext context)
        {
            Context = context;
        }

        public Poll Get(int id)
        {
            return Context.Polls.Single(a => a.Id == id);
        }

        public IEnumerable<Poll> GetAll()
        {
            return Context.Polls;
        }

        public IEnumerable<Poll> GetAll(string userId)
        {
            return Context.Polls.Where(p => p.UserId == userId);
        }

        public void Add(Poll poll)
        {
            Context.Polls.Add(poll);
        }

        public void Remove(Poll poll)
        {
            Context.Polls.Remove(poll);
        }

        public int Count()
        {
            return Context.Polls.Count();
        }

        public int Count(Expression<Func<Poll, bool>> expression)
        {
            return Context.Polls.Count(expression);
        }

        public IEnumerable<Answer> GetAnswers(int pollId)
        {
            return Context.Polls.Single(p => p.Id == pollId).Answers;
        }

        public IEnumerable<Answer> GetBestAnswers(int pollId)
        {
            var poll = Context.Polls.Single(p => p.Id == pollId);

            return poll.Answers
                .Where(a => a.Votes != 0
                        && a.Votes == poll.Answers
                                .OrderByDescending(an => an.Votes)
                                .First().Votes);
        }
    }
}