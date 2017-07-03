using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SimpleVoter;
using SimpleVoter.Controllers;
using SimpleVoter.Core;

namespace SimpleVoter.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        public HomeControllerTest()
        {
            var identity = new GenericIdentity("user1@domain.com");
            identity.AddClaim(new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", "user1@domain.com"));
            identity.AddClaim(new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", "1"));

            var principal = new GenericPrincipal(identity, null);

            var mockUoW = new Mock<IUnitOfWork>();
            var controller = new HomeController(mockUoW.Object);
            //controller.User = principal;
        }

        [TestMethod]
        public void Index()
        {
            // Arrange
            var mockUoW = new Mock<IUnitOfWork>();
            HomeController controller = new HomeController(mockUoW.Object);

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
