using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Helpers;
using Microsoft.Ajax.Utilities;
using SimpleVoter.Core;
using SimpleVoter.Core.Enums;
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

        public Poll GetSingle(int id)
        {
            return Context.Polls.Single(a => a.Id == id);
        }

        public IEnumerable<Poll> Get(string userId)
        {
            return Context.Polls.Where(p => p.UserId == userId).ToList();
        }

        public IEnumerable<Poll> GetAll(SortBy sortBy = SortBy.Id, SortDirection sortDirection = SortDirection.Ascending)
        {
            IEnumerable<Poll> polls = null;

            switch (sortBy)
            {
                case SortBy.Id:
                    polls = sortDirection == SortDirection.Ascending
                        ? Context.Polls.OrderBy(p => p.Id)
                        : Context.Polls.OrderByDescending(p => p.Id);
                    break;
                    case SortBy.Question:
                    polls = sortDirection == SortDirection.Ascending 
                        ? Context.Polls.OrderBy(p => p.Question)
                        : Context.Polls.OrderByDescending(p => p.Question);
                    break;
                    case SortBy.UserName:
                    polls = sortDirection == SortDirection.Ascending
                        ? Context.Polls.OrderBy(p => p.User.UserName)
                        : Context.Polls.OrderByDescending(p => p.User.UserName);
                    break;
            }

            return polls.ToList();
        }

        public IEnumerable<Poll> GetAll(string searchWord)
        {
            return Context.Polls.Where(p =>
                        p.Question.Contains(searchWord) ||
                        p.User.UserName.Contains(searchWord) ||
                        p.Id.ToString().Contains(searchWord) ||
                        searchWord == ""
                        ).ToList();
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
            return Context.Polls.Single(p => p.Id == pollId).Answers.ToList();
        }

        public IEnumerable<Answer> GetBestAnswers(int pollId)
        {
            var poll = Context.Polls.Single(p => p.Id == pollId);

            return poll.Answers
                .Where(a => a.Votes != 0
                        && a.Votes == poll.Answers
                                .OrderByDescending(an => an.Votes)
                                .First().Votes).ToList();
        }
    }
}