using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleVoter.Core.Models.Statistics
{
    public class UsersStatistics
    {
        public int TotalUsers { get; set; }
        public int UsersToday { get; set; }
        public int NewUsersToday { get; set; }
        public int DeletedUsersToday{ get; set; }
    }
}