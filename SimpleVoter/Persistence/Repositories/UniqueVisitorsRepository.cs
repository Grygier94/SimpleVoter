using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using SimpleVoter.Core;
using SimpleVoter.Core.Models;
using SimpleVoter.Core.Repositories;

namespace SimpleVoter.Persistence.Repositories
{
    public class UniqueVisitorsRepository : IUniqueVisitorsRepository
    {
        protected readonly IApplicationDbContext Context;
        public UniqueVisitorsRepository(IApplicationDbContext context)
        {
            Context = context;
        }

        public UniqueVisitor Get(string ip)
        {
            return Context.UniqueVisitors.Single(uv => uv.IpAdress == ip);
        }

        public bool Exists(string ip)
        {
            return Context.UniqueVisitors.Any(uv => uv.IpAdress == ip);
        }

        public void Add(UniqueVisitor visitor)
        {
            Context.UniqueVisitors.Add(visitor);
        }

        public bool HasAnsweredPoll(string ip, int pollId)
        {
            return Context.UniqueVisitors
                .Any(uv => uv.IpAdress == ip && uv.PollsParticipated.Select(p => p.Id).Contains(pollId));
        }
    }
}