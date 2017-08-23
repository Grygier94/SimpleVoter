using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using SimpleVoter.Core.Models;

namespace SimpleVoter.Core.ViewModels.AdminViewModels
{
    public class AdminDashboardViewModel
    {
        [Display(Name = "Total Users")]
        public int TotalUsers { get; set; }

        [Display(Name = "Users Today")]
        public int TotalUsersToday => NewUsersToday - DeletedUsersToday;

        [Display(Name = "New Users Today")]
        public int NewUsersToday { get; set; }

        [Display(Name = "Deleted Users Today")]
        public int DeletedUsersToday { get; set; }

        [Display(Name = "Total Public Polls")]
        public int TotalPublicPolls { get; set; }

        [Display(Name = "Public Polls Today")]
        public int PublicPollsToday => NewPublicPollsToday - DeletedPublicPollsToday;

        [Display(Name = "New Public Polls Today")]
        public int NewPublicPollsToday { get; set; }

        [Display(Name = "Deleted Public Polls Today")]
        public int DeletedPublicPollsToday { get; set; }

        [Display(Name = "Total Private Polls")]
        public int TotalPrivatePolls { get; set; }

        [Display(Name = "Private Polls Today")]
        public int PrivatePollsToday => NewPrivatePollsToday - DeletedPrivatePollsToday;

        [Display(Name = "New Private Polls Today")]
        public int NewPrivatePollsToday { get; set; }

        [Display(Name = "Deleted Private Polls Today")]
        public int DeletedPrivatePollsToday { get; set; }

        [Display(Name = "Total Unique Visitors")]
        public int TotalUniqueVisitors { get; set; }

        [Display(Name = "Unique Visitors Today")]
        public int UniqueVisitorsToday { get; set; }

        [Display(Name = "Total Page Views")]
        public int TotalPageViews { get; set; }

        [Display(Name = "Page Views Today")]
        public int PageViewsToday { get; set; }
    }
}