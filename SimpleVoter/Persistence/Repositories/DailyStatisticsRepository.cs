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

        public IEnumerable<DailyStatistics> GetRecordsFromLastDays(int numberOfDays)
        {
            var tresholdDate = DateTime.Now.AddDays(-numberOfDays);
            return Context.DailyStatistics
                .Where(ds => ds.Date >= tresholdDate)
                .ToList();
        }

        private DailyStatistics GetTodaysRecord()
        {
            var todayDate = DateTime.Now;
            var todaysStatistics = Context.DailyStatistics
                                       .SingleOrDefault(ds => ds.Date == todayDate.Date)
                                        ?? Context.DailyStatistics.Add(new DailyStatistics{Date = DateTime.Now.Date});
            return todaysStatistics;
        }

        #region GetTotalData
        public int GetTotalUsers()
        {
            return Context.DailyStatistics.Sum(ds => ds.Users).Value;
        }

        public int GetTotalPublicPolls()
        {
            return Context.DailyStatistics.Sum(ds => ds.PublicPolls).Value;
        }

        public int GetTotalPrivatePolls()
        {
            return Context.DailyStatistics.Sum(ds => ds.PrivatePolls).Value;
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