using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SimpleVoter.Core.Models;
using SimpleVoter.Persistence;

namespace SimpleVoter.Core.Filters
{
    public class ViewsCounterFilterAttribute :ActionFilterAttribute
    {
        private readonly IUnitOfWork _unitOfWork;

        public ViewsCounterFilterAttribute()
        {
            _unitOfWork = new UnitOfWork(new ApplicationDbContext());
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);

            if (HttpContext.Current.Session["PageViewed"] == null)
            {
                _unitOfWork.DailyStatistics.Increase_PageViews();
                _unitOfWork.Complete();
                HttpContext.Current.Session["PageViewed"] = true;
            }

            var userIp = HttpContext.Current.Request.UserHostAddress;
            if (!_unitOfWork.UniqueVisitors.Exists(userIp))
            {
                _unitOfWork.DailyStatistics.Increase_UniqueVisitors();
                _unitOfWork.UniqueVisitors.Add(new UniqueVisitor { IpAdress = userIp});
                _unitOfWork.Complete();
            }
        }
    }
}