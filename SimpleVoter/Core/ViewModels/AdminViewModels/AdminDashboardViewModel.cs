using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SimpleVoter.Core.Models;

namespace SimpleVoter.Core.ViewModels.AdminViewModels
{
    public class AdminDashboardViewModel
    {
        public int TotalUsers { get; set; }
        public int TotalUsersToday => NewUsersToday + DeletedUsersToday;
        public int NewUsersToday { get; set; }
        public int DeletedUsersToday { get; set; }

        public int TotalPublicPolls { get; set; }
        public int PublicPollsToday => NewPublicPollsToday + DeletedPublicPollsToday;
        public int NewPublicPollsToday { get; set; }
        public int DeletedPublicPollsToday { get; set; }

        public int TotalPrivatePolls { get; set; }
        public int PrivatePollsToday => NewPrivatePollsToday + DeletedPrivatePollsToday;
        public int NewPrivatePollsToday { get; set; }
        public int DeletedPrivatePollsToday { get; set; }
        
        public int TotalUniqueVisitors { get; set; }
        public int UniqueVisitorsToday { get; set; }
        public int TotalPageViews { get; set; }
        public int PageViewsToday { get; set; }
    }
}