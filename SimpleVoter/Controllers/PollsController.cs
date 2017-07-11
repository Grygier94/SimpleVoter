using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SimpleVoter.Core;
using SimpleVoter.Core.Models;

namespace SimpleVoter.Controllers
{
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
            return View();
        }

        public ActionResult DeletePoll(int pollId)
        {
            return View();
        }
    }
}