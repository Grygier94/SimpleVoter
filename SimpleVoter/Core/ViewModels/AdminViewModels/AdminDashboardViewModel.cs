using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SimpleVoter.Core.Models.Statistics;

namespace SimpleVoter.Core.ViewModels.AdminViewModels
{
    public class AdminDashboardViewModel
    {
        public UsersStatistics UsersStatistics { get; set; }
        public PollsStatistics PollsStatistics { get; set; }
        public PublicPollsStatistics PublicPollsStatistics { get; set; }
        public PrivatePollsStatistics PrivatePollsStatistics { get; set; }
        public UniqueVisitorsStatistics UniqueVisitorsStatistics { get; set; }
        public PageViewsStatistics PageViewsStatistics { get; set; }

    }
}