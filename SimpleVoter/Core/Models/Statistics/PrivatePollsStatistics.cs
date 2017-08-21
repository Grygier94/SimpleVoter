using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleVoter.Core.Models.Statistics
{
    public class PrivatePollsStatistics
    {
        public int TotalPrivatePolls { get; set; }
        public int PrivatePollsToday { get; set; }
        public int NewPrivatePollsToday { get; set; }
        public int DeletedPrivatePollsToday { get; set; }
    }
}