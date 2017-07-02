using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SimpleVoter.Core.Models;
using SimpleVoter.Core.Repositories;

namespace SimpleVoter.Persistence.Repositories
{
    public class AnswerRepository : RepositoryBase<Answer>, IAnswerRepository
    {
        public AnswerRepository(ApplicationDbContext context) : base(context) { }
    }
}