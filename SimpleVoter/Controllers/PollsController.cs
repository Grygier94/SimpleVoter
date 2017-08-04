using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Helpers;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json;
using SimpleVoter.Core;
using SimpleVoter.Core.Enums;
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

        //TODO: wyswietlic teksty jesli brak wynikow:
        //      | z szukanym tekstem - 'No results for given criteria' 
        //      | bez wyszukiwania (anonim) - 'No public polls created for anonymous users yet. You can create poll -here- or -login- to be able to see more polls.
        //      | bez wyszukiwania (zalogowany - 'No public polls craeted yet. Be the first one and create your own poll here!
        //TODO: dodac tooltipy do search bara
        //TODO: dodac tooltipy do numeru glosow 'votes'
        //TODO: przy tworzeniu możliwość wybrania typu wykresu (tylko zalogowani)
        //TODO: automatyczne usuwanie polla po czasie (wybor okresu przy tworzeniu, niezalogowani - zawsze po 24h)
        //TODO: możliwość wyboru public/private dla zalogowanych (private = dostep tylko przy pomocy linku)

        [AllowAnonymous]
        public ActionResult ShowAll()
        {
            var pagingInfo = new PagingInfo
            {
                ItemsPerPage = Int32.Parse(WebConfigurationManager.AppSettings["PollsPerPage"]),
                CurrentPage = 1
            };

            return View("PollList", pagingInfo);
        }

        [AllowAnonymous]
        public ActionResult RenderPollTable(string json)
        {
            PollTableInfo tableInfo = JsonConvert.DeserializeObject<PollTableInfo>(json);

            var polls = _unitOfWork.Polls.GetAll(tableInfo);

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

            return PartialView("_PollsTable", new Tuple<IEnumerable<PollListViewModel>, PagingInfo>(viewModelList, tableInfo.PagingInfo));
        }

        public ActionResult ShowUserPolls()
        {
            return View(_unitOfWork.Polls.Get(User.Identity.GetUserId()));
        }

        [AllowAnonymous]
        public ActionResult Details(int id)
        {
            var poll = _unitOfWork.Polls.GetSingle(id);
            return View(poll);
        }

        [AllowAnonymous]
        public void Vote(int[] ids)
        {
            foreach (var id in ids)
            {
                var answer = _unitOfWork.Answers.Get(id);
                answer.Votes++;
                _unitOfWork.Complete();
            }
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