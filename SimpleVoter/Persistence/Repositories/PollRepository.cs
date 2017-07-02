using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SimpleVoter.Core.Models;
using SimpleVoter.Core.Repositories;

namespace SimpleVoter.Persistence.Repositories
{
    public class PollRepository : RepositoryBase<Poll>, IPollRepository
    {
        public PollRepository(ApplicationDbContext context) : base(context) { }
    }
}