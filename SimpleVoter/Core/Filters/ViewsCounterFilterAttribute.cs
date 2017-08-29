using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject.Activation;
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
            if (HttpContext.Current.Request.Cookies["UniqueVisit"] == null &&
                _unitOfWork.UniqueVisitors.Exists(userIp))
            {
                var cookie = new HttpCookie("UniqueVisit", "false");
                cookie.Expires.AddYears(10);
                HttpContext.Current.Response.SetCookie(cookie);
            }

            if (HttpContext.Current.Request.Cookies["UniqueVisit"] == null && 
                !_unitOfWork.UniqueVisitors.Exists(userIp))
            {
                var cookie = new HttpCookie("UniqueVisit", "false");
                cookie.Expires.AddYears(10);
                HttpContext.Current.Response.SetCookie(cookie);
                _unitOfWork.DailyStatistics.Increase_UniqueVisitors();
                _unitOfWork.UniqueVisitors.Add(new UniqueVisitor { IpAdress = userIp});
                _unitOfWork.Complete();
            }
        }
    }
}