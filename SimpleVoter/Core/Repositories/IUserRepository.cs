using System.Collections.Generic;
using SimpleVoter.Core.Models;

namespace SimpleVoter.Core.Repositories
{
    public interface IUserRepository
    {
        ApplicationUser Get(string userId);
        IEnumerable<ApplicationUser> GetAll(PollTableInfo tableInfo);
    }
}