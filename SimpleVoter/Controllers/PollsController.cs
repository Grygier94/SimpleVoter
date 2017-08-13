using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
using SimpleVoter.Core.Extensions;
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

        //TODO: Paginacja poprzez wpisanie strony
        //TODO: _UserPollsTable - sortowanie po visibiity oraz osobne wyszukiwanie dla tabeli w panelu uzytkownika
        //TODO: walidacja daty wygasniecia (expiration date > datetime.now) przy tworzeniu polla przez zalogowanego uzytkownika oraz przy aktualizacji i przedluzeniu daty wygsniecia
        //TODO: ustawic min width dla number i visibility w tabeli uzytkownika oraz number i user w tabeli publicznej
        //TODO: panel admina
        //      - lista użytkowników
        //      - lista polli
        //      - możliwość edycji podstawowych danych / zablokowania / usunięcia użytkownika oraz usuniecie/zablokowanie polla
        //      - dashboard ze statystykami - wszyscy uzytkownicy | nowi uzytkownicy dzisiaj/w tygodniu/miesiacu | wszystkie polle | nowe polle
        //TODO: kolor :hover przyciskow przy logowaniu zewnetrznym: fb, twitter etc
        //TODO: przy tworzeniu możliwość wybrania typu wykresu (tylko zalogowani)
        //TODO: dodać visibility 'personal?' gdzie tylko zaproszeni przez tworce uzytkownicy moga glosowac
        //TODO: po kliknięciu scrollem na pozycje w tabeli - otworz w nowej karcie

        //TODO: automatyczne usuwanie polla po 24h - niezalogowani (sql server agent - job schedule)

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
            var pagingInfo = new PagingInfo
            {
                ItemsPerPage = Int32.Parse(WebConfigurationManager.AppSettings["PollsPerPage"]),
                CurrentPage = 1
            };

            return View("UserPollList", pagingInfo);
        }

        public ActionResult RenderUserPollTable(string json)
        {
            PollTableInfo tableInfo = JsonConvert.DeserializeObject<PollTableInfo>(json);

            var polls = _unitOfWork.Polls.GetAll(tableInfo, User.Identity.GetUserId());

            var viewModelList = new List<PollListViewModel>();
            var userManager = Request.GetOwinContext().GetUserManager<ApplicationUserManager>();

            foreach (var poll in polls)
            {
                viewModelList.Add(new PollListViewModel
                {
                    PollId = poll.Id,
                    Question = poll.Question,
                    UserName = poll.UserId == null ? "Anonymous" : userManager.Users.Single(u => u.Id == poll.UserId).UserName,
                    ExpirationDate = poll.ExpirationDate,
                    Visibility = poll.Visibility
                });
            }

            return PartialView("_UserPollsTable", new Tuple<IEnumerable<PollListViewModel>, PagingInfo>(viewModelList, tableInfo.PagingInfo));
        }

        [AllowAnonymous]
        public ActionResult Details(int id)
        {
            var poll = _unitOfWork.Polls.GetSingle(id);
            return View(poll);
        }


        [HttpGet]
        public ActionResult Update(int id)
        {
            var poll = _unitOfWork.Polls.GetSingle(id);
            var viewModel = new UpdateViewModel
            {
                Id = poll.Id,
                Question = poll.Question,
                AllowMultipleAnswers = poll.AllowMultipleAnswers,
                Answers = poll.Answers.ToList(),
                ExpirationDate = poll.ExpirationDate,
                Visibility = poll.Visibility
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Update(UpdateViewModel viewModel)
        {
            if (viewModel != null && ModelState.IsValid)
            {
                var poll = _unitOfWork.Polls.GetSingle(viewModel.Id);
                if (poll.Answers.All(a => a.Votes == 0))
                {
                    poll.Question = viewModel.Question;
                    _unitOfWork.Answers.RemoveRange(poll.Answers.ToList());
                    poll.Answers = viewModel.Answers
                        .Where(a => !string.IsNullOrWhiteSpace(a.Content))
                        .DistinctBy(a => a.Content).ToList();
                }

                poll.AllowMultipleAnswers = viewModel.AllowMultipleAnswers;
                poll.UpdateDate = DateTime.Now;
                poll.ExpirationDate = viewModel.ExpirationDate;
                poll.Visibility = viewModel.Visibility;

                _unitOfWork.Complete();
                return RedirectToAction("Details", new { id = poll.Id });
            }

            if (viewModel != null)
                viewModel.Answers = viewModel.Answers.Where(a => !a.Content.IsNullOrWhiteSpace()).ToList();

            return View(viewModel);
        }

        public ActionResult Delete(int id)
        {
            var poll = _unitOfWork.Polls.GetSingle(id);
            _unitOfWork.Polls.Remove(poll);
            _unitOfWork.Complete();

            return Json(new { success = true });
        }

        public ActionResult End(int id)
        {
            var poll = _unitOfWork.Polls.GetSingle(id);
            poll.ExpirationDate = DateTime.Now;
            poll.UpdateDate = DateTime.Now;
            poll.EndedByOwner = true;
            _unitOfWork.Complete();

            return Json(new { success = true });
        }

        public ActionResult Renew(int id, DateTime expirationDate)
        {
            var poll = _unitOfWork.Polls.GetSingle(id);
            poll.ExpirationDate = expirationDate;
            poll.UpdateDate = DateTime.Now;
            poll.RenewingDate = DateTime.Now;
            _unitOfWork.Complete();

            return Json(new { success = true });
        }

        [AllowAnonymous]
        public ActionResult Vote(int[] ids, DateTime? expirationDate)
        {
            if (expirationDate != null && DateTime.Now > expirationDate.Value)
                return Json(new { success = false, responseText = "Poll has expired." });

            foreach (var id in ids)
            {
                var answer = _unitOfWork.Answers.Get(id);
                answer.Votes++;
                _unitOfWork.Complete();
            }
            return Json(new { success = true });
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Create(CreateViewModel viewModel)
        {
            if (!User.Identity.IsAuthenticated)
            {
                viewModel.ExpirationDate = DateTime.Now.AddDays(1);
                viewModel.Visibility = Visibility.Public;
            }

            if (viewModel != null && ModelState.IsValid)
            {
                var poll = new Poll
                {
                    AllowMultipleAnswers = viewModel.AllowMultipleAnswers,
                    Question = viewModel.Question,
                    UserId = User.Identity.GetUserId(),
                    Answers = viewModel.Answers
                                .Where(a => !string.IsNullOrWhiteSpace(a.Content))
                                .DistinctBy(a => a.Content).ToList(),
                    CreationDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    ExpirationDate = viewModel.ExpirationDate,
                    Visibility = viewModel.Visibility
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