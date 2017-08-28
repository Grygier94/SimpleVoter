using System;
using System.Collections.Generic;
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

        public bool Exists(string ip)
        {
            return Context.UniqueVisitors.Any(uv => uv.IpAdress == ip);
        }

        public void Add(UniqueVisitor visitor)
        {
            Context.UniqueVisitors.Add(visitor);
        }
    }
}