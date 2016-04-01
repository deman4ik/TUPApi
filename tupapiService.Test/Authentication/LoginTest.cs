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
using LoginResult = tupapiService.Controllers.LoginResult;

namespace tupapiService.Test.Authentication
{
    [TestClass]
    public class LoginTest
    {
        private readonly ITupapiContext _testContext;
        private TestDbPopulator _testDbPopulator;
        private LoginController _controller;

        public LoginTest()
        {
            _testContext = new TestTupContext();
            _testDbPopulator = new TestDbPopulator(_testContext);
        }

        public LoginResult ParseLoginResponse(HttpResponseMessage response)
        {
            if (response == null)
            {
                Console.WriteLine("HttpResponseMessage is NULL");
                return null;
            }
            var result =  JsonConvert.DeserializeObject<LoginResult>(response.Content.ReadAsStringAsync().Result);
            if (result != null)
            {
                LogLoginResult(result);
            }
            return result;
        }

        public static void LogLoginResult(LoginResult result)
        {
            Console.WriteLine("# Authentication Token:");
            Console.WriteLine(result.AuthenticationToken);
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("# User Id:");
            Console.WriteLine(result.User.Id);
        }
        [TestInitialize]
        public void LoginTestInitialize()
        {
            _testDbPopulator.PopulateUsers(2);
            _testDbPopulator.PopulateStandartAccounts(2);
            var config = new HttpConfiguration();
            _controller = new LoginController(_testContext)
            {
                Request = new HttpRequestMessage()
            };
          
            _controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;
        }
        [TestMethod]
        public void CreateToken_ShouldReturnAuthToken()
        {
            var token = BaseAuth.CreateToken( "standart:u1");
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
            LoginResult result = ParseLoginResponse(response);
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.AuthenticationToken);
            Assert.AreEqual(result.User.Id, "u1");
        }


        [TestMethod]
        public void Login_ShouldReturnErrorIfRequestIsNull()
        {
            HttpResponseMessage response = _controller.Login(null);
            var result = TestHelper.ParseResponse(response);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, HttpStatusCode.Unauthorized.ToString());
            Assert.AreEqual(result.IsSuccessStatusCode, false);
            Assert.AreEqual(result.ApiResult, ApiResult.Validation);
            Assert.AreEqual(result.ErrorType, ErrorType.IsNull);
            Assert.AreEqual(result.ResponseMessage, "request");
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
            var result = TestHelper.ParseResponse(response);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, HttpStatusCode.Unauthorized.ToString());
            Assert.AreEqual(result.IsSuccessStatusCode, false);
            Assert.AreEqual(result.ApiResult, ApiResult.Validation);
            Assert.AreEqual(result.ErrorType, ErrorType.IsNull);
            Assert.AreEqual(result.ResponseMessage, nameof(req.Password));
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
            var result = TestHelper.ParseResponse(response);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, HttpStatusCode.Unauthorized.ToString());
            Assert.AreEqual(result.IsSuccessStatusCode, false);
            Assert.AreEqual(result.ApiResult, ApiResult.Validation);
            Assert.AreEqual(result.ErrorType, ErrorType.UserWithEmailorNameNotFound);
            Assert.AreEqual(result.ResponseMessage, req.Email);
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
            var result = TestHelper.ParseResponse(response);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, HttpStatusCode.Unauthorized.ToString());
            Assert.AreEqual(result.IsSuccessStatusCode, false);
            Assert.AreEqual(result.ApiResult, ApiResult.Validation);
            Assert.AreEqual(result.ErrorType, ErrorType.UserWithEmailorNameNotFound);
            Assert.AreEqual(result.ResponseMessage, req.Name);
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
            var result = TestHelper.ParseResponse(response);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, HttpStatusCode.Unauthorized.ToString());
            Assert.AreEqual(result.IsSuccessStatusCode, false);
            Assert.AreEqual(result.ApiResult, ApiResult.Denied);
            Assert.AreEqual(result.ErrorType, ErrorType.PasswordWrong);
            Assert.AreEqual(result.ResponseMessage, "user354pwd");
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
            var result = TestHelper.ParseResponse(response);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, HttpStatusCode.Unauthorized.ToString());
            Assert.AreEqual(result.IsSuccessStatusCode, false);
            Assert.AreEqual(result.ApiResult, ApiResult.Denied);
            Assert.AreEqual(result.ErrorType, ErrorType.UserBlocked);
        }
    }
}
