using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SimpleVoter.Controllers;
using SimpleVoter.Core;
using SimpleVoter.Core.Models;
using SimpleVoter.Core.Repositories;

namespace SimpleVoter.Tests.Controllers
{
    [TestClass]
    public class PollsControllerTest
    {
        private PollsController _pollsController;
        private Mock<IPollRepository> _mockRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockRepository = new Mock<IPollRepository>();
            var _mockUoW = new Mock<IUnitOfWork>();
            _mockUoW.SetupGet(u => u.Polls).Returns(_mockRepository.Object);
            _pollsController = new PollsController(_mockUoW.Object);
        }

        [TestMethod]
        public void ShowAll_ValidRequest_ShouldReturnView()
        {
            var result = _pollsController.ShowAll() as ViewResult;

            result.Should().NotBe(null);
            result.Should().BeOfType<ViewResult>();
            result.Model.Should().NotBe(null);
        }

        [TestMethod]
        public void Create_ValidRequest_ShouldCreatePollAndReturnView()
        {
            var poll = new Poll { Id = 1, Question = "Test question?", UserId = "1" };

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
            var result = _pollsController.Edit(null) as ViewResult;

            result.Should().NotBe(null);
            result.Should().BeOfType<ViewResult>();
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
