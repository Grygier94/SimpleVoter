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
    public class AdminController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public AdminController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult AdminDashboard()
        {
            return View();
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult UserDetails(string id)
        {
            var user = _unitOfWork.Users.Get(id);
            return View(user);
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