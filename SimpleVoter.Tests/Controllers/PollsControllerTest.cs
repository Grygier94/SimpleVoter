using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using FluentAssertions;
using Microsoft.AspNet.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SimpleVoter.Controllers;
using SimpleVoter.Core;
using SimpleVoter.Core.Models;
using SimpleVoter.Core.Repositories;
using SimpleVoter.Core.ViewModels;
using SimpleVoter.Core.ViewModels.PollViewModels;

namespace SimpleVoter.Tests.Controllers
{
    [TestClass]
    public class PollsControllerTest
    {
        private PollsController _pollsController;
        private Mock<IPollRepository> _mockPollRepository;
        private Mock<IAnswerRepository> _mockAnswerRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockPollRepository = new Mock<IPollRepository>();
            _mockAnswerRepository = new Mock<IAnswerRepository>();
            var _mockUoW = new Mock<IUnitOfWork>();

            _mockUoW.SetupGet(u => u.Polls).Returns(_mockPollRepository.Object);
            _mockUoW.SetupGet(u => u.Answers).Returns(_mockAnswerRepository.Object);
            _pollsController = new PollsController(_mockUoW.Object);
        }

        [TestMethod]
        public void ShowAll_ValidRequest_ShouldReturnView()
        {
            var result = _pollsController.ShowPublicPolls() as ViewResult;

            result.Should().NotBe(null);
            result.Should().BeOfType<ViewResult>();
        }

        [TestMethod]
        public void Create_ValidRequest_ShouldCreatePollAndReturnView()
        {
            var controllerContext = new Mock<ControllerContext>();
            var principal = new  Mock<IPrincipal>();
            principal.Setup(p => p.IsInRole("Administrator")).Returns(true);
            principal.SetupGet(x => x.Identity.Name).Returns("test");
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(principal.Object);
            _pollsController.ControllerContext = controllerContext.Object;

            var poll = new CreateViewModel
            {
                Question = "Test question?",
                AllowMultipleAnswers = true,
                Answers = new List<Answer> { new Answer { Id = 1, Content = "content", PollId = 1} }
            };

            var result = _pollsController.Create(poll) as RedirectToRouteResult;

            result.Should().NotBe(null);
            result.Should().BeOfType<RedirectToRouteResult>();
            result.RouteValues["Action"].Should().Be("Details");
        }

        [TestMethod]
        public void Create_InvalidRequest_ShouldReturnCreateView()
        {
            var result = _pollsController.Create(null) as ViewResult;

            result.Should().NotBe(null);
            result.Should().BeOfType<ViewResult>();
        }

        [TestMethod]
        public void Edit_InvalidRequest_ShouldReturnEditView()
        {
            //var result = _pollsController.Edit(null) as ViewResult;

            //result.Should().NotBe(null);
            //result.Should().BeOfType<ViewResult>();
        }


        [TestMethod]
        public void Details_ValidRequest_ShouldReturnDetailsView()
        {
            var result = _pollsController.Details(1) as ViewResult;

            result.Should().NotBe(null);
            result.Should().BeOfType<ViewResult>();
        }
    }
}
