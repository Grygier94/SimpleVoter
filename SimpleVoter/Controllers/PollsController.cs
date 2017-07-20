using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
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

        [AllowAnonymous]
        public ActionResult ShowAll()
        {
            return View(_unitOfWork.Polls);
        }

        public ActionResult ShowUserPolls()
        {
            return View(_unitOfWork.Polls.GetAll(User.Identity.GetUserId()));
        }

        [AllowAnonymous]
        public ActionResult Details(int id = 1)
        {
            var poll = _unitOfWork.Polls.Get(id);
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
                    UserId = viewModel.UserId,
                    Answers = viewModel.Answers.Where(a => !string.IsNullOrWhiteSpace(a.Content)).Distinct().ToList()
                };

                if (poll.Answers.Count < 2)
                {
                    ModelState.AddModelError("Answers", "Question must contains at least 2 answers!");
                    return View(viewModel);
                }

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
                var pollFromDb = _unitOfWork.Polls.Get(poll.Id);
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