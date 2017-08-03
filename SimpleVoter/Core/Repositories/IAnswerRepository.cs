using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SimpleVoter.Core.Models;

namespace SimpleVoter.Core.Repositories
{
    public interface IAnswerRepository
    {
        Answer Get(int id);
        int GetVotes(int answerId);
    }
}