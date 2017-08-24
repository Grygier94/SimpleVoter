using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleVoter.Core.Models
{
    public class DailyStatistics
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }

        public int NewUsers { get; set; }
        public int DeletedUsers { get; set; }
        public int? Users {
            get { return NewUsers - DeletedUsers; }
            private set { }
        }

        public int NewPublicPolls { get; set; }
        public int DeletedPublicPolls { get; set; }

        public int? PublicPolls
        {
            get { return NewPublicPolls - DeletedPublicPolls; }
            private set { }
        }

        public int NewPrivatePolls { get; set; }
        public int DeletedPrivatePolls { get; set; }
        public int? PrivatePolls
        {
            get { return NewPrivatePolls - DeletedPrivatePolls; }
            private set { }
        }

        public int UniqueVisitors  { get; set; }
        public int PageViews { get; set; }
    }
}