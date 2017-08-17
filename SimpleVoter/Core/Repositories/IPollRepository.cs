using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Helpers;
using SimpleVoter.Core.Enums;
using SimpleVoter.Core.Models;

namespace SimpleVoter.Core.Repositories
{
    public interface IPollRepository
    {
        Poll GetSingle(int id);
        IEnumerable<Poll> GetAll(PollTableInfo tableInfo, string userId = "", bool isAdmin = false);
        IEnumerable<Poll> GetAll(string searchWord);
        void Add(Poll poll);
        void Remove(Poll poll);
        int Count();
        int Count(Expression<Func<Poll, bool>> expression);
        IEnumerable<Answer> GetAnswers(int pollId);
        IEnumerable<Answer> GetBestAnswers(int pollId);
    }
}