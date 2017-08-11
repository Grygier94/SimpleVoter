using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SimpleVoter.Core;
using SimpleVoter.Core.Models;
using SimpleVoter.Core.Repositories;

namespace SimpleVoter.Persistence.Repositories
{
    public class AnswerRepository : IAnswerRepository
    {
        protected readonly IApplicationDbContext Context;
        public AnswerRepository(IApplicationDbContext context)
        {
            Context = context;
        }

        public Answer Get(int id)
        {
            return Context.Answers.Single(a => a.Id == id);
        }

        public int GetVotes(int answerId)
        {
            return Context.Answers.Single(a => a.Id == answerId).Votes;
        }

        public void Remove(Answer answer)
        {
            Context.Answers.Remove(answer);
        }

        public void RemoveRange(List<Answer> answers)
        {
            for (var i = 0; i < answers.Count; i++)
                Context.Answers.Remove(answers[i]);
        }
    }
}