using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using tupapi.Shared.DataObjects;
using tupapi.Shared.Enums;
using tupapiService.Authentication;
using tupapiService.Controllers;
using tupapiService.Helpers.DBHelpers;
using tupapiService.Models;
using tupapiService.Test.Infrastructure;
using LoginResult = tupapiService.DataObjects.LoginResult;

namespace tupapiService.Test.Authentication
{
    [TestClass]
    public class LoginTest : BaseControllerTest
    {
        private LoginController _controller;


        [TestInitialize]
        public void LoginTestInitialize()
        {
            TestDbPopulator.PopulateUsers(2);
            TestDbPopulator.PopulateStandartAccounts(2);
            var config = new HttpConfiguration();
            _controller = new LoginController(TestContext)
            {
                Request = new HttpRequestMessage()
            };

            _controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;
        }

        [TestMethod]
        public void CreateToken_ShouldReturnAuthToken()
        {
            var token = BaseAuth.CreateToken("standart:u1");
            Debug.WriteLine("# Token : ");
            Debug.WriteLine(token);
        }

        [TestMethod]
        public void Login_ShouldReturnToken()
        {
            var req = new StandartAuthRequest
            {
                Email = "user1@example.com",
                Password = "user1pwd"
            };
            HttpResponseMessage response = _controller.Login(req);
            TestResult<LoginResult> result = TestHelper.ParseLoginResponse(response);
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Data.AuthenticationToken);
            Assert.AreEqual(result.Data.User.Id, "u1");
        }


        [TestMethod]
        public void Login_ShouldReturnErrorIfRequestIsNull()
        {
            HttpResponseMessage response = _controller.Login(null);
            var result = TestHelper.ParseErorResponse(response);
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.Unauthorized.ToString(), result.StatusCode);
            Assert.AreEqual(false, result.IsSuccessStatusCode);
            Assert.AreEqual(ApiResult.Validation, result.ApiResult);
            Assert.AreEqual(ErrorType.IsNull, result.Data.ErrorType);
            Assert.AreEqual("request", result.Data.Message);
        }

        [TestMethod]
        public void Login_ShouldReturnErrorIfPasswordIsNull()
        {
            var req = new StandartAuthRequest
            {
                Email = "user1@example.com",
                Password = null
            };
            HttpResponseMessage response = _controller.Login(req);
            var result = TestHelper.ParseErorResponse(response);
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.Unauthorized.ToString(), result.StatusCode);
            Assert.AreEqual(false, result.IsSuccessStatusCode);
            Assert.AreEqual(ApiResult.Validation, result.ApiResult);
            Assert.AreEqual(ErrorType.IsNull, result.Data.ErrorType);
            Assert.AreEqual(nameof(req.Password), result.Data.Message);
        }

        [TestMethod]
        public void Login_ShouldReturnErrorIfUserWithEmailIsNotFound()
        {
            var req = new StandartAuthRequest
            {
                Email = "user354@example.com",
                Password = "user354pwd"
            };
            HttpResponseMessage response = _controller.Login(req);
            var result = TestHelper.ParseErorResponse(response);
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.Unauthorized.ToString(), result.StatusCode);
            Assert.AreEqual(false, result.IsSuccessStatusCode);
            Assert.AreEqual(ApiResult.Validation, result.ApiResult);
            Assert.AreEqual(ErrorType.UserWithEmailorNameNotFound, result.Data.ErrorType);
            Assert.AreEqual(req.Email, result.Data.Message);
        }

        [TestMethod]
        public void Login_ShouldReturnErrorIfUserWithNameIsNotFound()
        {
            var req = new StandartAuthRequest
            {
                Name = "user354",
                Password = "user354pwd"
            };
            HttpResponseMessage response = _controller.Login(req);
            var result = TestHelper.ParseErorResponse(response);
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.Unauthorized.ToString(), result.StatusCode);
            Assert.AreEqual(false, result.IsSuccessStatusCode);
            Assert.AreEqual(ApiResult.Validation, result.ApiResult);
            Assert.AreEqual(ErrorType.UserWithEmailorNameNotFound, result.Data.ErrorType);
            Assert.AreEqual(req.Name, result.Data.Message);
        }

        [TestMethod]
        public void Login_ShouldReturnErrorIfPasswordIsWrong()
        {
            var req = new StandartAuthRequest
            {
                Name = "user1",
                Password = "user354pwd"
            };
            HttpResponseMessage response = _controller.Login(req);
            var result = TestHelper.ParseErorResponse(response);
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.Unauthorized.ToString(), result.StatusCode);
            Assert.AreEqual(false, result.IsSuccessStatusCode);
            Assert.AreEqual(ApiResult.Denied, result.ApiResult);
            Assert.AreEqual(ErrorType.PasswordWrong, result.Data.ErrorType);
            Assert.AreEqual("user354pwd", result.Data.Message);
        }

        [TestMethod]
        public void Login_ShouldReturnErrorIfUserIsBlocked()
        {
            var req = new StandartAuthRequest
            {
                Email = "user2@example.com",
                Password = "user2pwd"
            };
            HttpResponseMessage response = _controller.Login(req);
            var result = TestHelper.ParseErorResponse(response);
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.Unauthorized.ToString(), result.StatusCode);
            Assert.AreEqual(false, result.IsSuccessStatusCode);
            Assert.AreEqual(ApiResult.Denied, result.ApiResult);
            Assert.AreEqual(ErrorType.UserBlocked, result.Data.ErrorType);
        }
    }
}