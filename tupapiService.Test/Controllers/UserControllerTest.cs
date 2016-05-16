using System;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Web.Http;
using System.Web.Http.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using tupapi.Shared.DataObjects;
using tupapiService.Authentication;
using tupapiService.Controllers;
using tupapiService.Test.Infrastructure;
using User = tupapiService.Models.User;

namespace tupapiService.Test.Controllers
{
    [TestClass]
    public class UserControllerTest : BaseControllerTest
    {
        private UserApiController _controller;
        private User _user;

        [TestInitialize]
        public void UserControllerTestInitialize()
        {
        }

        [TestMethod]
        public void UserController_ShouldReturnCurrentUser()
        {
            TestDbPopulator.PopulateUsers(2);
            TestDbPopulator.PopulateStandartAccounts(2);
            _user = TestDbPopulator.GetUser(1);
            var req = new StandartAuthRequest
            {
                Email = _user.Email,
                Password = "user1pwd"
            };

            string token = BaseAuth.CreateToken("u1");
            var config = new HttpConfiguration();
            var request = new HttpRequestMessage {RequestUri = new Uri("http://localhost:50268/api/User")};
            request.Headers.Add("x-zumo-auth", token);
            request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;
            var username = "u1";
            var identity = new GenericIdentity(username, "");
            var nameIdentifierClaim = new Claim(ClaimTypes.NameIdentifier, username);
            identity.AddClaim(nameIdentifierClaim);
            var principal = new GenericPrincipal(identity, new string[] {});
            var user = new ClaimsPrincipal(principal);
            _controller = new UserApiController(TestContext)
            {
                Request = request,
                User = user
            };
            var response = _controller.GetCurrentUser();
            var result = TestHelper.ParseUserResponse(response);
            Assert.AreEqual("OK", result.StatusCode);
        }
    }
}