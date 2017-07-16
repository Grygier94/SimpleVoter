using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using SimpleVoter.Core;
using SimpleVoter.Core.Models;

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
        public ActionResult Details(int pollId)
        {
            var poll = _unitOfWork.Polls.Get(pollId);
            return View(poll);
        }


        [AllowAnonymous]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Create(Poll poll)
        {
            if (poll != null && ModelState.IsValid)
            {
                _unitOfWork.Polls.Add(poll);
                _unitOfWork.Complete();

                return RedirectToAction("Details", poll.Id);
            }

            return View(poll);
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