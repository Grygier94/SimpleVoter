using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SimpleVoter.Core;
using SimpleVoter.Core.Models;
using SimpleVoter.Core.Repositories;

namespace SimpleVoter.Persistence.Repositories
{
    public class DailyStatisticsRepository : IDailyStatisticsRepository
    {
        protected readonly IApplicationDbContext Context;
        public DailyStatisticsRepository(IApplicationDbContext context)
        {
            Context = context;
        }

        public DailyStatistics GetTodays()
        {
            return GetTodaysRecord();
        }

        private DailyStatistics GetTodaysRecord()
        {
            var todayDate = DateTime.Now;
            var todaysStatistics = Context.DailyStatistics
                                       .SingleOrDefault(ds => ds.Date.Year == todayDate.Year
                                                              && ds.Date.Month == todayDate.Month
                                                              && ds.Date.Day == todayDate.Day) 
                                        ?? Context.DailyStatistics.Add(new DailyStatistics());
            return todaysStatistics;
        }

        #region GetTotalData
        public int GetTotalUsers()
        {
            return Context.DailyStatistics.Sum(ds => ds.Users);
        }

        public int GetTotalPublicPolls()
        {
            return Context.DailyStatistics.Sum(ds => ds.PublicPolls);
        }

        public int GetTotalPrivatePolls()
        {
            return Context.DailyStatistics.Sum(ds => ds.PrivatePolls);
        }

        public int GetTotalUniqueVisitors()
        {
            return Context.DailyStatistics.Sum(ds => ds.UniqueVisitors);
        }

        public int GetTotalPageViews()
        {
            return Context.DailyStatistics.Sum(ds => ds.PageViews);
        }
        #endregion
        #region IncreaseData
        public void Increase_NewUsers()
        {
            var todaysStatistics = GetTodaysRecord();
            todaysStatistics.NewUsers++;
        }

        public void Increase_DeletedUsers()
        {
            var todaysStatistics = GetTodaysRecord();
            todaysStatistics.DeletedUsers++;
        }

        public void Increase_NewPublicPolls()
        {
            var todaysStatistics = GetTodaysRecord();
            todaysStatistics.NewPublicPolls++;
        }

        public void Increase_DeletedPublicPolls()
        {
            var todaysStatistics = GetTodaysRecord();
            todaysStatistics.DeletedPublicPolls++;
        }

        public void Increase_NewPrivatePolls()
        {
            var todaysStatistics = GetTodaysRecord();
            todaysStatistics.NewPrivatePolls++;
        }

        public void Increase_DeletedPrivatePolls()
        {
            var todaysStatistics = GetTodaysRecord();
            todaysStatistics.DeletedPrivatePolls++;
        }

        public void Increase_UniqueVisitors()
        {
            var todaysStatistics = GetTodaysRecord();
            todaysStatistics.UniqueVisitors++;
        }

        public void Increase_PageViews()
        {
            var todaysStatistics = GetTodaysRecord();
            todaysStatistics.PageViews++;
        }
        #endregion
    }
}