using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using tupapi.Shared.DataObjects;
using tupapiService.Controllers;
using tupapiService.Helpers.DBHelpers;
using tupapiService.Models;
using tupapiService.Test.Infrastructure;
using LoginResult = tupapiService.Controllers.LoginResult;
using User = tupapiService.Models.User;

namespace tupapiService.Test.Controllers
{
    [TestClass]
    public class UserControllerTest : BaseControllerTest
    {
        private UserController _controller;
        private User _user;

        [TestInitialize]
        public void UserControllerTestInitialize()
        {
            TestDbPopulator.PopulateUsers(2);
            TestDbPopulator.PopulateStandartAccounts(2);
            _user = TestDbPopulator.GetUser(1);
            var req = new StandartAuthRequest
            {
                Email = _user.Email,
                Password = "user1pwd"
            };

            LoginResult loginResult = TestHelper.Authenticate(TestContext, req);
            var config = new HttpConfiguration();
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri("http://localhost:50268/api/User");
            request.Headers.Add("x-zumo-auth", loginResult.AuthenticationToken);
            request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;
            _controller = new UserController(TestContext)
            {
                Request = request
            };
           
        }

        [TestMethod]
        public void UserController_ShouldReturnCurrentUser()
        {
            var response = _controller.GetCurrentUser();
            var result = TestHelper.ParseBaseResponse(response);
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }
    }
}