using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using SimpleVoter.Core;
using SimpleVoter.Core.Models;
using SimpleVoter.Core.ViewModels;

namespace SimpleVoter.Controllers
{
    [Authorize]
    public class PollsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public PollsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //TODO: utworzyc polllist view model z imieniem uzytkownika i tytulem polla oraz poll id
        [AllowAnonymous]
        public ActionResult ShowAll()
        {
            return View("PollList");
        }

        [AllowAnonymous]
        //[ChildActionOnly]
        public ActionResult RenderPollTable(string searchText = "")
        {
            var polls = _unitOfWork.Polls.GetAll(searchText);
            var viewModelList = new List<PollListViewModel>();
            var userManager = Request.GetOwinContext().GetUserManager<ApplicationUserManager>();

            foreach (var poll in polls)
            {
                viewModelList.Add(new PollListViewModel
                {
                    PollId = poll.Id,
                    Question = poll.Question,
                    UserName = poll.UserId == null ? "Anonymous" : userManager.Users.Single(u => u.Id == poll.UserId).UserName
                });
            }

            return PartialView("_PollsTable", viewModelList);
        }

        public ActionResult ShowUserPolls()
        {
            return View(_unitOfWork.Polls.Get(User.Identity.GetUserId()));
        }

        [AllowAnonymous]
        public ActionResult Details(int id = 1)
        {
            var poll = _unitOfWork.Polls.GetSingle(id);
            return View(poll);
        }

        [AllowAnonymous]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Create(CreateViewModel viewModel)
        {
            if (viewModel != null && ModelState.IsValid)
            {
                var poll = new Poll
                {
                    AllowMultipleAnswers = viewModel.AllowMultipleAnswers,
                    Question = viewModel.Question,
                    UserId = User.Identity.GetUserId(),
                    Answers = viewModel.Answers
                                .Where(a => !string.IsNullOrWhiteSpace(a.Content))
                                .DistinctBy(a => a.Content).ToList()
                };

                _unitOfWork.Polls.Add(poll);
                _unitOfWork.Complete();

                return RedirectToAction("Details", new { id = poll.Id });
            }

            return View(viewModel);
        }

        public ActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Edit(Poll poll)
        {
            if (poll != null && ModelState.IsValid)
            {
                var pollFromDb = _unitOfWork.Polls.GetSingle(poll.Id);
                pollFromDb.Answers = poll.Answers;
                pollFromDb.Question = poll.Question;

                return RedirectToAction("Details", poll.Id);
            }

            return View(poll);
        }

        public ActionResult DeletePoll(int pollId)
        {
            return View();
        }
    }
}