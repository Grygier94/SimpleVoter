using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SimpleVoter.Core.Models;

namespace SimpleVoter.Core.Repositories
{
    public interface IUniqueVisitorsRepository
    {
        bool Exists(string ip);
        void Add(UniqueVisitor visitor);
        bool HasAnsweredPoll(string ip, int pollId);
        UniqueVisitor Get(string ip);
    }
}