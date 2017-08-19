using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SimpleVoter.Core.Repositories;

namespace SimpleVoter.Core
{
    public interface IUnitOfWork
    {
        IPollRepository Polls { get; }
        IAnswerRepository Answers { get; }
        IUserRepository Users { get; }
        void Complete();
    }
}