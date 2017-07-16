using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using SimpleVoter.Core.Models;

namespace SimpleVoter.Core.Repositories
{
    public interface IPollRepository
    {
        Poll Get(int id);
        IEnumerable<Poll> GetAll();
        IEnumerable<Poll> GetAll(string userId);
        void Add(Poll poll);
        void Remove(Poll poll);
        int Count();
        int Count(Expression<Func<Poll, bool>> expression);
        IEnumerable<Answer> GetAnswers(int pollId);
        IEnumerable<Answer> GetBestAnswers(int pollId);
    }
}