using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleVoter.Core.Models.Statistics
{
    public class PollsStatistics
    {
        public int TotalPolls { get; set; }
        public int PollsToday { get; set; }
        public int NewPollsToday { get; set; }
        public int DeletedPollsToday { get; set; }
    }
}