using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json;
using SimpleVoter.Core;
using SimpleVoter.Core.Models;
using SimpleVoter.Core.ViewModels.PollViewModels;

namespace SimpleVoter.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult AdminDashboard()
        {
            return View();
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult ShowUsers()
        {
            var pagingInfo = new PagingInfo
            {
                ItemsPerPage = int.Parse(WebConfigurationManager.AppSettings["PollsPerPage"]),
                CurrentPage = 1
            };

            return View("UsersList", pagingInfo);
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult RenderUsersTable(string json)
        {
            var tableInfo = JsonConvert.DeserializeObject<PollTableInfo>(json);

            var users = _unitOfWork.Users.GetAll(tableInfo);

            return PartialView("_UsersTable", new Tuple<IEnumerable<ApplicationUser>, PagingInfo>(users, tableInfo.PagingInfo));
        }
    }
}