using System.Collections.Generic;
using SimpleVoter.Core.Models;

namespace SimpleVoter.Core.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<ApplicationUser> GetAll(PollTableInfo tableInfo);
    }
}