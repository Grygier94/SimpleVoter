using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleVoter.Core.Models.Statistics
{
    public class PublicPollsStatistics
    {
        public int TotalPublicPolls { get; set; }
        public int PublicPollsToday { get; set; }
        public int NewPublicPollsToday { get; set; }
        public int DeletedPublicPollsToday { get; set; }
    }
}